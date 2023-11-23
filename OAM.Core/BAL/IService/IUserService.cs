using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.BAL.IService
{
   public interface IUserService
    {
        Task<ApiBaseResponse> Register(RegisterRequest registerRequest);
    }
}
