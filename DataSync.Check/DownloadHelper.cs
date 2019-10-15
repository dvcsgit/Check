using Check;
using ICSharpCode.SharpZipLib.Zip;
using Models.Check.DataSync;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using Utility;
using Utility.Models;

namespace DataSync.Check
{
    public class DownloadHelper : IDisposable
    {
        private string Guid;

        private DownloadDataModel downloadDataModel = new DownloadDataModel();

        public RequestResult Generate(DownloadFormModel downloadFormModel)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                requestResult = Init();

                if (requestResult.IsSuccess)
                {
                    requestResult = Query(downloadFormModel);

                    if (requestResult.IsSuccess)
                    {
                        requestResult = GenerateSQLite();

                        if (requestResult.IsSuccess)
                        {
                            requestResult = GenerateZip();

                            if (!requestResult.IsSuccess)
                            {
                                Logger.Log("GenerateZip Failed!");
                            }
                        }
                        else
                        {
                            Logger.Log("GenerateSQLite Failed!");
                        }
                    }
                    else
                    {
                        Logger.Log("Query Failed!");
                    }
                }
                else
                {
                    Logger.Log("Init Failed!");                    
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                //throw;
            }

            return requestResult;
        }

        public RequestResult Init()
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                Guid = System.Guid.NewGuid().ToString();                
                Directory.CreateDirectory(GeneratedFolderPath);                
                File.Copy(TemplateDbFilePath, GeneratedDbFilePath);
                requestResult.Success();
            }
            catch (Exception e)
            {                
                var error = new Error(MethodBase.GetCurrentMethod(), e);                
                Logger.Log(error);
                requestResult.ReturnError(error);
                //throw;
            }

            return requestResult;
        }

        private RequestResult Query(DownloadFormModel downloadFormModel)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                using(CheckContext context=new CheckContext())
                {
                    var people = context.People.Select(p=>new PersonModel
                    {
                        PersonId=p.PersonId.ToString(),
                        LoginId=p.LoginId,
                        Name=p.Name,
                        Password=p.Password,
                        Title=p.Title,
                        Section=p.Section
                    }).ToList();

                    downloadDataModel.PersonModels.AddRange(people);

                    foreach(var parameter in downloadFormModel.DownloadParameters)
                    {
                        downloadDataModel.EquipmentModels.AddRange(context.Equipments.Where(x => x.EquipmentId == new Guid(parameter.EquipmentId)).Select(e => new EquipmentModel
                        {
                            EquipmentId = e.EquipmentId.ToString(),
                            EId = e.EId,
                            Position = e.Position,
                            Type = e.Type,
                            OrganizationId = e.OrganizationId.ToString(),
                            PersonId = e.PersonId.ToString()
                        }).ToList());
                    }
                    //downloadDataModel.EquipmentModels = context.Equipments.Where(x=>x.EquipmentId==).Select(e => new EquipmentModel
                    //{
                    //    EquipmentId = e.EquipmentId.ToString(),
                    //    EId = e.EId,
                    //    Position = e.Position,
                    //    Type = e.Type,
                    //    OrganizationId = e.OrganizationId.ToString(),
                    //    PersonId = e.PersonId.ToString()
                    //}).ToList();

                    downloadDataModel.AbnormalReasonModels = context.AbnormalReasons.Select(a => new AbnormalReasonModel
                    {
                        AbnormalReasonId = a.AbnormalReasonId.ToString(),
                        ARId = a.ARId,
                        Reason = a.Reason,
                        OrganizationId = a.OrganizationId.ToString(),
                        PersonId = a.PersonId.ToString()
                    }).ToList();
                }

                requestResult.Success();
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                //throw;
            }

            return requestResult;
        }

        private RequestResult GenerateSQLite()
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                using(SQLiteConnection conn=new SQLiteConnection(SQLiteConnString))
                {
                    conn.Open();

                    using(SQLiteTransaction trans = conn.BeginTransaction())
                    {
                        foreach(var person in downloadDataModel.PersonModels)
                        {
                            using(SQLiteCommand command = conn.CreateCommand())
                            {
                                command.CommandText = @"INSERT INTO People (
                       PersonId,
                       LoginId,
                       Name,
                       Password,
                       Title,                       
                       Section
                   )
                   VALUES (
                       @PersonId,
                       @LoginId,
                       @Name,
                       @Password,
                       @Title,                      
                       @Section
                   )";
                                command.Parameters.AddWithValue("PersonId", person.PersonId);
                                command.Parameters.AddWithValue("LoginId",person.LoginId);
                                command.Parameters.AddWithValue("Name",person.Name);
                                command.Parameters.AddWithValue("Password",person.Password);
                                command.Parameters.AddWithValue("Title",person.Title);                                
                                command.Parameters.AddWithValue("Section",person.Section);

                                command.ExecuteNonQuery();
                            }
                        }

                        foreach (var equipment in downloadDataModel.EquipmentModels)
                        {
                            using (SQLiteCommand command = conn.CreateCommand())
                            {
                                command.CommandText = @"INSERT INTO Equipment (
                          EquipmentId,
                          EId,
                          Position,
                          Type,
                          OrganizationId,
                          PersonId
                      )
                      VALUES (
                          @EquipmentId,
                          @EId,
                          @Position,
                          @Type,
                          @OrganizationId,
                          @PersonId
                      )";
                                command.Parameters.AddWithValue("EquipmentId",equipment.EquipmentId);
                                command.Parameters.AddWithValue("EId",equipment.EId);
                                command.Parameters.AddWithValue("Position",equipment.Position);
                                command.Parameters.AddWithValue("Type",equipment.Type);
                                command.Parameters.AddWithValue("OrganizationId",equipment.OrganizationId);
                                command.Parameters.AddWithValue("PersonId",equipment.PersonId);
                                
                                command.ExecuteNonQuery();
                            }
                        }

                        foreach (var abnormalReason in downloadDataModel.AbnormalReasonModels)
                        {
                            using (SQLiteCommand command = conn.CreateCommand())
                            {
                                command.CommandText = @"INSERT INTO AbnormalReason (
                               AbnormalReasonId,
                               ARId,
                               Reason,
                               OrganizationId,
                               PersonId
                           )
                           VALUES (
                               @AbnormalReasonId,
                               @ARId,
                               @Reason,
                               @OrganizationId,
                               @PersonId
                           )";
                                command.Parameters.AddWithValue("AbnormalReasonId",abnormalReason.AbnormalReasonId);
                                command.Parameters.AddWithValue("ARId",abnormalReason.ARId);
                                command.Parameters.AddWithValue("Reason",abnormalReason.Reason);
                                command.Parameters.AddWithValue("OrganizationId",abnormalReason.OrganizationId);
                                command.Parameters.AddWithValue("PersonId",abnormalReason.PersonId);
                                
                                command.ExecuteNonQuery();
                            }
                        }

                        //foreach(var equipment in downloadDataModel.EquipmentModels)
                        //{
                        //    using (SQLiteCommand command = conn.CreateCommand())
                        //    {
                        //        command.CommandText = @"";
                        //        command.Parameters.AddWithValue("",);
                        //        command.ExecuteNonQuery();
                        //    }
                        //}

                        trans.Commit();
                    }

                    conn.Close();
                }

                requestResult.Success();
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                throw;
            }

            return requestResult;
        }

        private RequestResult GenerateZip()
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                using (FileStream fs = File.Create(GeneratedZipFilePath))
                {
                    using(ZipOutputStream zipOutputStream=new ZipOutputStream(fs))
                    {
                        zipOutputStream.SetLevel(9);
                        ZipHelper.CompressFolder(GeneratedDbFilePath, zipOutputStream);
                        zipOutputStream.IsStreamOwner = true;
                        zipOutputStream.Close();
                    }
                }

                requestResult.ReturnData(GeneratedZipFilePath);
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                //throw;
            }

            return requestResult;
        }

        private string GeneratedFolderPath
        {
            get
            {
                return Path.Combine(Config.CheckSQLiteGeneratedFolderPath, Guid);
            }
        }

        private string TemplateDbFilePath
        {
            get
            {
                return Path.Combine(Config.CheckSQLiteTemplateFolderPath, Define.SQLite_Check);
            }
        }       

        private string GeneratedDbFilePath
        {
            get
            {
                return Path.Combine(GeneratedFolderPath, Define.SQLite_Check);
            }
        }

        private string GeneratedZipFilePath
        {
            get
            {
                return Path.Combine(GeneratedFolderPath, Define.SQLiteZip_Check);
            }
        }

        private string SQLiteConnString
        {
            get
            {
                return string.Format("Data Source={0};Version=3;", GeneratedDbFilePath);
            }
        }

        #region IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }

            _disposed = true;
        }

        ~DownloadHelper()
        {
            Dispose(false);
        }

        #endregion
    }
}
