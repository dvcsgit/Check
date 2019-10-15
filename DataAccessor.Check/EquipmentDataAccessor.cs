using Check;
using Check.Models;
using Models.Authentication;
using Models.Check.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utility;
using Utility.Models;

namespace DataAccessor.Check
{
    public class EquipmentDataAccessor
    {
        public static RequestResult GetEquipments(Account account,string flag)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                var equipments = new List<EquipmentModel>();

                using (CheckContext context = new CheckContext())
                {
                    var organization = context.Organizations.Where(o => o.OrganizationId == account.OrganizationId).FirstOrDefault();
                    var rootOrganization = context.Organizations.Where(o => o.OrganizationId == account.OrganizationId).FirstOrDefault();                    
                    if (rootOrganization.ParentId == new Guid())
                    {
                        equipments = context.Equipments.Select(e => new EquipmentModel()
                        {
                            EquipmentId = e.EquipmentId.ToString(),
                            EId = e.EId,
                            Position = e.Position,
                            Department = e.Organization.Name,                            
                            Enable = e.Enable,
                            Creator = e.Person.Name,
                            LastModifyTime = e.LastModifyTime
                        }).ToList();
                    }
                    else
                    {
                        equipments = context.Equipments.Where(e => e.Type == flag).Select(e => new EquipmentModel()
                        {
                            EquipmentId = e.EquipmentId.ToString(),
                            EId = e.EId,
                            Position = e.Position,
                            Department = e.Organization.Name,                           
                            Enable = e.Enable,
                            Creator = e.Person.Name,
                            LastModifyTime = e.LastModifyTime
                        }).ToList();
                    }

                }
                requestResult.ReturnData(equipments);
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
            }

            return requestResult;
        }

        public static RequestResult EditEquipment(EquipmentModel equipmentModel)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                using (CheckContext context = new CheckContext())
                {
                    var equipment = context.Equipments.Where(e => e.EquipmentId == new Guid(equipmentModel.EquipmentId)).FirstOrDefault();
                    equipment.EId = equipmentModel.EId;
                    equipment.Position = equipmentModel.Position;
                    equipment.Type = equipmentModel.Type;
                    equipment.Enable = equipmentModel.Enable;
                    equipment.OrganizationId = new Guid(equipmentModel.Department);                    
                    //context.Entry(equipment).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    requestResult.ReturnSuccessMessage("Successful");
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
            }

            return requestResult;
        }

        public static RequestResult CreateEquipment(EquipmentModel equipmentModel, Account account)
        {
            RequestResult requestResult = new RequestResult();
            try
            {
                using (CheckContext context = new CheckContext())
                {
                    context.Equipments.Add(new Equipment
                    {
                        EId = equipmentModel.EId,
                        Position = equipmentModel.Position,
                        Type = equipmentModel.Type,
                        Person = context.People.Where(p => p.LoginId == account.Id).FirstOrDefault(),
                        OrganizationId = new Guid(equipmentModel.Department),                        
                        Enable = equipmentModel.Enable,
                        LastModifyTime = DateTime.Now
                    });
                    context.SaveChanges();

                    requestResult.ReturnSuccessMessage("Successful");
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
            }

            return requestResult;
        }

        public static RequestResult DeleteEquipment(string equipmentId)
        {
            RequestResult requestResult = new RequestResult();
            try
            {
                using (CheckContext context = new CheckContext())
                {
                    context.Equipments.Remove(context.Equipments.Find(new Guid(equipmentId)));
                    context.SaveChanges();

                    requestResult.ReturnSuccessMessage("Successful");
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
            }

            return requestResult;
        }        

        public static RequestResult GetDepartments(Account account)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                var departments = new List<DepartmentModel>();
                using (CheckContext context = new CheckContext())
                {
                    var parentFactory = context.Organizations.Where(o => o.OrganizationId == account.OrganizationId).FirstOrDefault();
                    var rootOrganization = context.Organizations.Where(o => o.OrganizationId == account.OrganizationId).FirstOrDefault();
                    if (rootOrganization.ParentId == new Guid())
                    {
                        departments = context.Organizations.Select(e => new DepartmentModel()
                        {
                            DepartmentId = e.OrganizationId.ToString(),
                            DepartmentName = e.Name
                        }).ToList();
                    }
                    else
                    {
                        departments = context.Organizations.Where(o => o.OrganizationId == account.OrganizationId).Select(e => new DepartmentModel()
                        {
                            DepartmentId = e.OrganizationId.ToString(),
                            DepartmentName = e.Name
                        }).ToList();
                    }

                }
                requestResult.ReturnData(departments);
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
            }

            return requestResult;
        }       
    }
}
