using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models.API_Responses;

namespace OAM.Core.DAL.IRepository
{
    public interface IUserRepository
    {
        Task<RegisterResponse> Register(User user);
        Task<List<UserDetails>> GetUser(int? userId);
    }
}
