using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.BAL.IService
{
    public interface ICommonService
    {
        bool IsValidIpAddress(string IpAddress);
    }
}
