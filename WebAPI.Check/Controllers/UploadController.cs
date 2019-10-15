using DataSync.Check;
using Models.Check.DataSync;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Utility;
using Utility.Models;

namespace WebAPI.Check.Controllers
{
    public class UploadController : ApiController
    {
        public HttpResponseMessage Post()
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            
            try
            {               
                var request = HttpContext.Current.Request;
                
                if (request.Form.Count != 0)
                {
                    string jsonStringValue = request.Form[0];
                    
                    Logger.Log(jsonStringValue);
                    
                    var formInput = JsonConvert.DeserializeObject<ApiFormInput>(jsonStringValue);
                    
                    var postedFile = request.Files[0];
                    
                    var guid = Guid.NewGuid();
                    
                    var folder = Path.Combine(Config.CheckSQLiteUploadFolderPath, guid.ToString());

                    Directory.CreateDirectory(folder);
                    
                    postedFile.SaveAs(Path.Combine(folder, "Check.Upload.zip"));

                    RequestResult requestResult = UploadHelper.Upload(guid, formInput);

                    if (requestResult.IsSuccess)
                    {
                        httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, Message = requestResult.Message });
                    }
                    else
                    {
                        httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, new { IsSuccess = false, Message = requestResult.Error.ErrorMessage });
                    }
                }
                else
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, new { IsSuccess = false });
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, new { Success = false, Message = error.ErrorMessage });
                //throw;
            }

            return httpResponseMessage;
        }
    }
}
