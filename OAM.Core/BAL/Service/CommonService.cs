using Microsoft.AspNetCore.Http;
using OAM.Core.BAL.IService;
using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.BAL.Service
{
    public class CommonService:ICommonService
    {
        //Declaration
        private readonly ICommonRepository _commonRepository;

        //Constructor 
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        #region To Check Whether the Request IP is Valid or Not
        public bool IsValidIpAddress(string IpAddress)
        {
            return _commonRepository.IsValidIpAddress(IpAddress);
        }
        #endregion

        #region Get App Settings
        public string GetAppSettings(string appKey)
        {
            return _commonRepository.GetAppSettings(appKey); 
        }
        #endregion

        #region To Save Request and Response Log
        public long SaveApiRequestResposelog(ApiLogEntryResponse apiLogEntry)
        {
            return _commonRepository.SaveApiRequestResposelog(apiLogEntry);
        }
        #endregion
    }
}
