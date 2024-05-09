using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Helpers;
using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static OAM.Core.Enums.Enums;

namespace OAM.Core.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        //Declaration
        private readonly IConfiguration _config;
        //Constructor
        public UserRepository(IConfiguration configuration)
        {
            _config = configuration;
        }
        #region Register New User
        public async Task<RegisterResponse> Register(User user)
        {
            RegisterResponse response = new RegisterResponse();
            response.Status = HttpStatusCode.OK.ToString();
            response.StatusCode = (int)HttpStatusCode.OK;
            if (user != null)
            {
                using (var entities = new OamDevContext(_config))
                {
                    User? dbuser = entities.Users.Where(x => x.Id == user.Id).SingleOrDefault();
                    if (dbuser != null)
                    {
                        //Update Logic If ID Exists
                        dbuser.UserName = user.UserName;
                        dbuser.PhoneNumber = user.PhoneNumber;
                        dbuser.PasswordSalt = user.PasswordSalt;
                        dbuser.PasswordHash = user.PasswordHash;
                        dbuser.UpdatedTimeStamp = DateTime.Now;
                        response.Message = Utility.GetEnumDisplayName(StatusMessage.Updated);
                    }
                    else
                    {
                        //Check Is User Already Exists
                        bool isDuplicate = entities.Users.Where(x => x.Email.ToLower() == user.Email.ToLower() && x.UserName.ToLower() == user.UserName.ToLower()).Any();
                        if (isDuplicate)
                        {
                            response.Status = HttpStatusCode.BadRequest.ToString();
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            response.Message = Utility.GetEnumDisplayName(StatusMessage.DuplicateUser);
                        }
                        else
                        {
                            //Save Logic If ID doesn't Exists
                            entities.Users.Add(user);
                            response.Message = Utility.GetEnumDisplayName(StatusMessage.Saved);
                        }
                    }
                    entities.SaveChanges();
                }
                response.userDetails = new UserDetails()
                {
                    UserName = user.UserName,
                    UserId = Utility.GetGuid(user.UserId),
                    EmailAddress = user.Email,
                    CreatedTime = DateTime.Now
                };
            }
            return response;
        }
        #endregion

        #region Get User Details
        public async Task<List<UserDetails>> GetUser(int? userId)
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            using (var entities = new OamDevContext(_config))
            {
                userDetails = (from u in entities.Users
                               select new UserDetails
                               {
                                   UserName = u.UserName,
                                   EmailAddress = u.Email,
                                   CreatedTime = DateTime.Now,
                                   UserId = Utility.GetGuid(u.UserId)
                               }).AsNoTracking().ToList();
            }
            return userDetails;
        }
        #endregion

        #region Login
        public async Task<UserDetails> Login(OAM.Core.Models.Base_Models.API_Requests.LoginRequest login)
        {
            using (var entities = new OamDevContext(_config))
            {
                UserDetails userDetails = (from u in entities.Users
                                           where u.Email == login.Username && u.IsDeleted != true
                                           select new UserDetails
                                           {
                                               UserId = Utility.GetGuid(u.UserId),
                                               PasswordHash = u.PasswordHash,
                                               PasswordSalt = u.PasswordSalt,
                                               UserName = u.UserName,
                                               EmailAddress = login.Email
                                           }).AsNoTracking().SingleOrDefault();

                bool isValid = VerifyPassword(login.Password, userDetails.PasswordHash, userDetails.PasswordSalt);
                if (isValid)
                {
                    return userDetails;
                }
                else
                {
                    return new UserDetails();
                }

            }
        }
        private bool VerifyPassword(string enteredPassword, byte[] storedPasswordHash, byte[] storedPasswordSalt)
        {
            using (var hmac = new HMACSHA512(storedPasswordSalt))
            {
                byte[] enteredPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(enteredPassword));

                // Compare the computed hash with the stored hash
                for (int i = 0; i < storedPasswordHash.Length; i++)
                {
                    if (storedPasswordHash[i] != enteredPasswordHash[i])
                        return false;
                }
                return true;
            }
        }
        #endregion
    }
}
