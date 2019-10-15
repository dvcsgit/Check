using Check.Models;
using DataAccessor;
using DataAccessor.Check;
using Models.Authentication;
using Models.Check.Equipment;
using Models.Shared;
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
    public class EquipmentController : Controller
    {
        // GET: Check/Equipment
        public ActionResult Index()
        {
            return View(new QueryFormModel());
        }

        public JsonResult GetEquipments(string sidx, string sort, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var equipments = EquipmentDataAccessor.GetEquipments(Session["Account"] as Account, "Manufacture").Data as List<EquipmentModel>;

            if (_search)
            {
                switch (searchField)
                {
                    case "EId":
                        equipments = equipments.Where(t => t.EId.Contains(searchString)).ToList();
                        break;
                }
            }
            int totalRecords = equipments.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                equipments = equipments.OrderByDescending(t => t.EId).ToList();
                equipments = equipments.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                equipments = equipments.OrderBy(t => t.EId).ToList();
                equipments = equipments.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = equipments
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }    

        public ActionResult Edit(EquipmentModel equipmentModel)
        {
            
            RequestResult requestResult = new RequestResult();
            try
            {
                if (ModelState.IsValid)
                {
                    requestResult = EquipmentDataAccessor.EditEquipment(equipmentModel);
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

        public ActionResult Create(EquipmentModel equipmentModel)
        {
            equipmentModel.Type = "Manufacture";
            RequestResult requestResult = new RequestResult();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    requestResult = EquipmentDataAccessor.CreateEquipment(equipmentModel, Session["Account"] as Account);
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
                requestResult = EquipmentDataAccessor.DeleteEquipment(id);
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

        public JsonResult GetOptions()
        {            
            var departments = EquipmentDataAccessor.GetDepartments(Session["Account"] as Account).Data as List<DepartmentModel>;
            return Json(departments, JsonRequestBehavior.AllowGet);
        }               
    }
}