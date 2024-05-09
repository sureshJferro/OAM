using OAM.Core.BAL.IService;
using OAM.Core.DAL.IRepository;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models.API_Responses;
using System.Security.Cryptography;

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

        #region  Register New User
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Id = request.Id,
                UserId = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1 //Default As Admin
            };
            return await _userRepository.Register(user);
        }
        #endregion

        #region Create Hash Password
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion

        #region Get User Details
        public async Task<List<UserDetails>> GetUser(int? userId)
        {
            return await _userRepository.GetUser(userId);
        }
        #endregion

        #region Login
        public async Task<UserDetails> Login(LoginRequest login)
        {
            return await _userRepository.Login(login);
        }
        #endregion

    }
}
