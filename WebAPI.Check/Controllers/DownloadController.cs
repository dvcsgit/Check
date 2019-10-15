using DataSync.Check;
using Models.Check.DataSync;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Utility;
using Utility.Models;

namespace WebAPI.Check.Controllers
{
    public class DownloadController : ApiController
    {
        public HttpResponseMessage Post(DownloadFormModel downloadFormModel)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            try
            {
                using(DownloadHelper downloadHelper=new DownloadHelper())
                {
                    Logger.Log(Newtonsoft.Json.JsonConvert.SerializeObject(downloadFormModel));

                    RequestResult requestResult = downloadHelper.Generate(downloadFormModel);

                    if (requestResult.IsSuccess)
                    {
                        httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                        httpResponseMessage.Content = new StreamContent(new FileStream(requestResult.Data.ToString(), FileMode.Open));
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = Define.SQLiteZip_Check
                        };
                    }
                    else
                    {
                        httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, new { IsSuccess = false, Message = requestResult.Message });
                    }
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, new { IsSuccess = false, Message = error.ErrorMessage });
                //throw;
            }

            return httpResponseMessage;
        }
    }
}
