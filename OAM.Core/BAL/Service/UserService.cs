using Azure;
using OAM.Core.BAL.IService;
using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.BAL.Service
{
    public class UserService:IUserService
    {
        //Declaration
        public readonly IUserRepository _userRepository;

        //Constructor
        public UserService(IUserRepository userRepository)
        {
           _userRepository = userRepository;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Id = request.Id,
                Email = request.Email,
                UserName = request.Name,
                PasswordHash =passwordHash,
                PasswordSalt =passwordSalt,
                RoleId = 1
            };
            return await _userRepository.Register(user);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
