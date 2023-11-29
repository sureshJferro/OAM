using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Helpers;
using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
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
        public async Task<RegisterResponse> Register(User user)
        {
            RegisterResponse response = new RegisterResponse();
            response.Status = HttpStatusCode.OK.ToString();
            response.StatusCode = (int)HttpStatusCode.OK;
            if (user != null)
            {
                
                User? dbuser = _devContext.Users.Where(x => x.Id == user.Id).SingleOrDefault();
                if (dbuser != null)
                {
                    //Update Logic If ID Exists
                    dbuser.UserName = user.UserName;
                    dbuser.PasswordSalt = user.PasswordSalt;
                    dbuser.PasswordHash = user.PasswordHash;
                    dbuser.UpdatedTimeStamp = DateTime.Now;
                    response.Message = Utility.GetEnumDisplayName(StatusMessage.Updated);
                }
                else
                {
                    //Check Is User Already Exists
                    bool isDuplicate = _devContext.Users.Where(x => x.Email.ToLower() == user.Email.ToLower() && x.UserName.ToLower() == user.UserName.ToLower()).Any();
                    if (isDuplicate)
                    {
                        response.Status = HttpStatusCode.BadRequest.ToString();
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Message = Utility.GetEnumDisplayName(StatusMessage.DuplicateUser);
                    }
                    else
                    {
                        //Save Logic If ID doesn't Exists
                        _devContext.Users.Add(user);
                        response.Message = Utility.GetEnumDisplayName(StatusMessage.Saved);
                    }
                }
                _devContext.SaveChanges();
                response.userDetails = new UserDetails()
                {
                    Name = user.UserName,
                    UserId = Utility.GetGuid(user.UserId),
                    EmailAddress = user.Email,
                    CreatedTime = DateTime.Now
                };
            }
            return response;
        }
    }
}
