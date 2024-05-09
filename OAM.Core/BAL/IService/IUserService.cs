using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.BAL.IService
{
   public interface IUserService
    {
        Task<RegisterResponse> Register(RegisterRequest registerRequest);
        Task<List<UserDetails>> GetUser(int? userid);
        Task<UserDetails> Login(LoginRequest login);
    }
}
