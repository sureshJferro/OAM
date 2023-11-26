using Microsoft.Extensions.Configuration;
using OAM.Core.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.DAL.Repository
{
    public class CommonRepository:ICommonRepository
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
    }
}
