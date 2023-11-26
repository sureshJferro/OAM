using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.DAL.IRepository
{
    public interface ICommonRepository
    {
        bool IsValidIpAddress(string IpAddress);
    }
}
