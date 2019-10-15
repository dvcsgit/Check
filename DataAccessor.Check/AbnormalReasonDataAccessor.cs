using Check;
using Check.Models;
using Models.Authentication;
using Models.Check.AbnormalReason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Models;

namespace DataAccessor.Check
{
    public class AbnormalReasonDataAccessor
    {
        public static RequestResult GetAbnormalReasons(Account account)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                var abnormalReasons = new List<AbnormalReasonModel>();

                using (CheckContext context = new CheckContext())
                {
                    abnormalReasons = context.AbnormalReasons.Select(e => new AbnormalReasonModel()
                    {
                        AbnormalReasonId = e.AbnormalReasonId.ToString(),
                        ARId=e.ARId,
                        Reason = e.Reason,
                        Dept=e.Organization.Name,
                        Enable = e.Enable,
                        Creator = e.Person.Name,
                        LastModifyTime = e.LastModifyTime
                    }).ToList();

                }
                requestResult.ReturnData(abnormalReasons);
            }
            catch (Exception e)
            {
                var error = new Error(MethodBase.GetCurrentMethod(), e);
                Logger.Log(error);
                requestResult.ReturnError(error);
            }

            return requestResult;
        }

        public static RequestResult Edit(AbnormalReasonModel abnormalReasonModel)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                using (CheckContext context = new CheckContext())
                {
                    var abnormalReason = context.AbnormalReasons.Where(e => e.AbnormalReasonId == new Guid(abnormalReasonModel.AbnormalReasonId)).FirstOrDefault();
                    abnormalReason.Reason = abnormalReasonModel.Reason;
                    abnormalReason.Enable = abnormalReasonModel.Enable;                                        
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

        public static RequestResult Create(AbnormalReasonModel abnormalReasonModel, Account account)
        {
            RequestResult requestResult = new RequestResult();
            try
            {
                using (CheckContext context = new CheckContext())
                {
                    context.AbnormalReasons.Add(new AbnormalReason
                    {   
                        ARId=abnormalReasonModel.ARId,
                        OrganizationId=account.OrganizationId,
                        Reason = abnormalReasonModel.Reason,                        
                        Person = context.People.Where(p => p.LoginId == account.Id).FirstOrDefault(),                        
                        Enable = abnormalReasonModel.Enable,
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

        public static RequestResult Delete(string abnormalReasonId)
        {
            RequestResult requestResult = new RequestResult();
            try
            {
                using (CheckContext context = new CheckContext())
                {
                    context.AbnormalReasons.Remove(context.AbnormalReasons.Find(new Guid(abnormalReasonId)));
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
    }
}
