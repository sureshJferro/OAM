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
        #region To Check Whether the Request IP is Valid or Not
        public bool IsValidIpAddress(string IpAddress)
        {
            bool IsValidIpAddress = false;
            return IsValidIpAddress;
        }
        #endregion
    }
}
