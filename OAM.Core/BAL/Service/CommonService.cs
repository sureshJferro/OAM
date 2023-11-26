using OAM.Core.BAL.IService;
using OAM.Core.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
