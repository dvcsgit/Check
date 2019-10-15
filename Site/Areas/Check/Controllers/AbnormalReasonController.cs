using DataAccessor.Check;
using Models.Authentication;
using Models.Check.AbnormalReason;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Utility;
using Utility.Models;

namespace Site.Areas.Check.Controllers
{
    public class AbnormalReasonController : Controller
    {
        // GET: Check/AbnormalReason
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAbnormalReasons(string sidx, string sort, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var abnormalReasons = AbnormalReasonDataAccessor.GetAbnormalReasons(Session["Account"] as Account).Data as List<AbnormalReasonModel>;

            if (_search)
            {
                switch (searchField)
                {
                    case "Reason":
                        abnormalReasons = abnormalReasons.Where(t => t.Reason.Contains(searchString)).ToList();
                        break;
                }
            }
            int totalRecords = abnormalReasons.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                abnormalReasons = abnormalReasons.OrderByDescending(t => t.Reason).ToList();
                abnormalReasons = abnormalReasons.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                abnormalReasons = abnormalReasons.OrderBy(t => t.Reason).ToList();
                abnormalReasons = abnormalReasons.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = abnormalReasons
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(AbnormalReasonModel abnormalReasonModel)
        {

            RequestResult requestResult = new RequestResult();
            try
            {
                if (ModelState.IsValid)
                {
                    requestResult = AbnormalReasonDataAccessor.Edit(abnormalReasonModel);
                }

                if (requestResult.IsSuccess)
                {
                    requestResult.ReturnSuccessMessage("Successful");
                }
                else
                {
                    requestResult.ReturnFailedMessage("failed");
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                //throw;
            }
            return Content(JsonConvert.SerializeObject(requestResult));
        }

        public ActionResult Create(AbnormalReasonModel abnormalReasonModel)
        {           
            RequestResult requestResult = new RequestResult();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    requestResult = AbnormalReasonDataAccessor.Create(abnormalReasonModel, Session["Account"] as Account);
                    if (requestResult.IsSuccess)
                    {
                        requestResult.ReturnSuccessMessage("Successful");
                        msg = "Saved Successfully";
                    }
                    else
                    {
                        requestResult.ReturnFailedMessage("failed");
                        msg = "Validation data not successfully";
                    }
                }
                else
                {
                    msg = "Validation data not successfully";
                }

            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                msg = "Error occured:" + e.Message;
                //throw;
            }

            return Content(JsonConvert.SerializeObject(requestResult));
            //return msg;
        }

        public ActionResult Delete(string id)
        {
            //var para= HttpContext.Request.
            string sFullParams = HttpContext.ApplicationInstance.Context.Request.Params.ToString();
            string sUrlPara = sFullParams.Substring(0, sFullParams.IndexOf("&ALL_HTTP="));

            RequestResult requestResult = new RequestResult();
            try
            {
                requestResult = AbnormalReasonDataAccessor.Delete(id);
                if (requestResult.IsSuccess)
                {
                    requestResult.ReturnSuccessMessage("Successful");
                }
                else
                {
                    requestResult.ReturnFailedMessage("failed");
                }
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
                //throw;
            }

            return Content(JsonConvert.SerializeObject(requestResult));
        }       
    }
}