using Check;
using Check.Models;
using Models.Check.DataSync;
using System;
using System.Linq;
using System.Reflection;
using Utility;
using Utility.Models;

namespace DataSync.Check
{
    public class UploadHelper
    {
        public static RequestResult Upload(Guid guid,ApiFormInput apiFormInput)
        {
            RequestResult requestResult = new RequestResult();

            try
            {
                using (CheckContext context = new CheckContext())
                {
                    context.UploadLogs.Add(new UploadLog()
                    {
                        UploadLogId = guid,
                        UploadPId = apiFormInput != null ? apiFormInput.PId : "",
                        UploadTime = DateTime.Now
                    });

                    context.SaveChanges();

                    requestResult.Success();

                    try
                    {
                        if (apiFormInput != null)
                        {
                            if (!string.IsNullOrEmpty(apiFormInput.MacAddress))
                            {
                                var status = context.DeviceStatuses.FirstOrDefault(x => x.MacAddress == apiFormInput.MacAddress);

                                if (status != null)
                                {
                                    status.AppVersion = apiFormInput.AppVersion;
                                    status.IMEI = apiFormInput.IMEI;
                                    status.LastUpdateTime = DateTime.Now;
                                }
                                else
                                {
                                    status = new DeviceStatus()
                                    {
                                        DeviceStatusId = Guid.NewGuid(),
                                        AppVersion = apiFormInput.AppVersion,
                                        IMEI = apiFormInput.IMEI,
                                        MacAddress = apiFormInput.MacAddress,
                                        LastUpdateTime = DateTime.Now
                                    };

                                    context.DeviceStatuses.Add(status);
                                }
                            }
                            else if (!string.IsNullOrEmpty(apiFormInput.IMEI))
                            {
                                var status = context.DeviceStatuses.FirstOrDefault(x => x.IMEI == apiFormInput.IMEI);
                                if (status != null)
                                {
                                    status.AppVersion = apiFormInput.AppVersion;
                                    status.MacAddress = apiFormInput.MacAddress;
                                    status.LastUpdateTime = DateTime.Now;
                                }
                                else
                                {
                                    status = new DeviceStatus()
                                    {
                                        DeviceStatusId = Guid.NewGuid(),
                                        AppVersion = apiFormInput.AppVersion,
                                        IMEI = apiFormInput.IMEI,
                                        MacAddress = apiFormInput.MacAddress,
                                        LastUpdateTime = DateTime.Now
                                    };

                                    context.DeviceStatuses.Add(status);
                                }
                            }

                            context.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Log(MethodBase.GetCurrentMethod(), e);
                        //throw;
                    }
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
    }
}
