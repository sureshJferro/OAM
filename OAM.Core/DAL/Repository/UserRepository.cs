using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static OAM.Core.Enums.Enums;

namespace OAM.Core.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        //Declaration
        private readonly OamDevContext _devContext;

        //Constructor
        public UserRepository(OamDevContext devContext)
        {
            _devContext = devContext;
        }
        public async Task<ApiBaseResponse> Register(User user)
        {
            ApiBaseResponse apiBaseResponse = new ApiBaseResponse();
            _devContext.Users.Add(user);
            _devContext.SaveChanges();
            if (user.Id > 0)
            {
                apiBaseResponse.Status =HttpStatusCode.OK.ToString();
                apiBaseResponse.StatusCode =(int)HttpStatusCode.OK;
                apiBaseResponse.Message = StatusMessage.Success.ToString();
            }
            else
            {
                apiBaseResponse.Status =HttpStatusCode.BadRequest.ToString();
                apiBaseResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                apiBaseResponse.Message = StatusMessage.Bad_Request.ToString();
            }

            return apiBaseResponse;
        }
    }
}
