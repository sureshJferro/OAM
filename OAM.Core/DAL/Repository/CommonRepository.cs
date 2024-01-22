using Microsoft.Extensions.Configuration;
using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.DAL.Repository
{
    public class CommonRepository : ICommonRepository
    {
        //Declaration
        private readonly IConfiguration _config;


        //Constructor
        public CommonRepository(IConfiguration config)
        {
            _config = config;
        }

        #region To Check Whether the Request IP is Valid or Not
        public bool IsValidIpAddress(string IpAddress)
        {
            bool IsValidIpAddress = false;
            return IsValidIpAddress;
        }
        #endregion

        #region Get App Settings
        public string GetAppSettings(string appKey)
        {
            object value = null;
            value = _config.GetSection("AppSettings:" + appKey).Value;
            if (value != null)
            {
                return value.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region To Save Request and Response Log
        public long SaveApiRequestResposelog(ApiLogEntryResponse requestResponseLog)
        {
            var dat = _config.GetConnectionString(Helpers.Constants.OAMConnection);
            bool IsExists = false;
            long RequestResposelogId = 0;
            ApiRequestResponseLog? dbrequestResponseLog = null;
            if (requestResponseLog != null)
            {
                using (var entities = new OamDevContext(_config))
                {
                    dbrequestResponseLog = entities.ApiRequestResponseLogs.Where(x=>x.LogId==requestResponseLog.APIRequestId).SingleOrDefault();
                    if (dbrequestResponseLog!=null)
                    {
                        IsExists=true;
                    }
                    else
                    {
                        dbrequestResponseLog = new ApiRequestResponseLog();
                    }
                    
                    dbrequestResponseLog.ResponseStatusCode=requestResponseLog.ResponseStatusCode;
                    dbrequestResponseLog.ResponseBody=requestResponseLog.ResponseContentBody;
                    dbrequestResponseLog.UpdateTimeStamp = DateTime.Now;
                    if (!IsExists)
                    {
                        dbrequestResponseLog.RequestMethod = requestResponseLog.RequestMethod;
                        dbrequestResponseLog.RequestBody = requestResponseLog.RequestContentBody;
                        dbrequestResponseLog.RequestMethod = requestResponseLog.ActionName;
                        dbrequestResponseLog.RequestPath = requestResponseLog.RequestActionMethod;
                        dbrequestResponseLog.CreateTimeStamp = DateTime.Now;
                        entities.ApiRequestResponseLogs.Add(dbrequestResponseLog);
                    }
                    entities.SaveChanges();
                    RequestResposelogId = dbrequestResponseLog.LogId;
                }
            }
            return RequestResposelogId;
        }
        #endregion
    }
}
