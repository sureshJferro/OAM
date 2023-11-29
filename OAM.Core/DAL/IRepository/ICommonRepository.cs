using OAM.Core.Entities;
using OAM.Core.Models.Base_Models.API_Responses;
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
        string GetAppSettings(string appKey);
        long SaveApiRequestResposelog(ApiLogEntryResponse requestResponseLog);
    }
}
