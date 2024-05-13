using Microsoft.AspNetCore.Mvc;
using OAM.Core.BAL.IService;
using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models.API_Responses;
using System.Net;
using helpers = OAM.Core.Helpers;
namespace OAM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableRateLimiting("fixed")]
    public class UserController : ControllerBase
    {
        //Declaration
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        //Constructor
        public UserController( IConfiguration configuration,IUserService userService) { 
        
             _configuration = configuration;
             _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {

            RegisterResponse registerResponse = new RegisterResponse();
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            else
            {
                registerResponse = await _userService.Register(registerRequest);
            }
            //For Postman Status Code
            if (registerResponse.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                 BadRequest(registerResponse);
            }
            return registerResponse;
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<RegisterResponse> UpdateUser(RegisterRequest updateUser)
        {
            RegisterResponse updateUserResponse = new RegisterResponse();
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            else
            {
                updateUserResponse = await _userService.Register(updateUser);
            }
            return updateUserResponse;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<List<UserDetails>> GetUser(int? userId)
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            //if (uuserId>0)
            //{
                userDetails = await _userService.GetUser(userId);
            //}
            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return userDetails;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<UserDetails> Login(LoginRequest login)
        {
            UserDetails userDetails = new UserDetails();
            if (login!=null && !string.IsNullOrWhiteSpace(login.Username) && !string.IsNullOrWhiteSpace(login.Password))
            {
                 userDetails = await _userService.Login(login);
                if (userDetails!=null && userDetails.UserId!=Guid.Empty)
                {
                   
                    userDetails.Status=HttpStatusCode.OK.ToString();
                    userDetails.StatusCode= (int)HttpStatusCode.OK;
                    userDetails.Message = helpers.Constants.Success;
                }
                else
                {
                    userDetails.Status = HttpStatusCode.BadRequest.ToString();
                    userDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                    userDetails.Message = helpers.Constants.NotFound.ToString();
                }
            }
            else
            {
                userDetails.Status = HttpStatusCode.BadRequest.ToString();
                userDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                userDetails.Message = helpers.Constants.CredentialsRequired.ToString();
            }
            HttpContext.Response.StatusCode = userDetails.StatusCode;
            return userDetails;
        }
    }
}
