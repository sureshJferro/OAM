using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OAM.Core.BAL.IService;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Requests;
using OAM.Core.Models.Base_Models.API_Responses;
using System.Net;

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
            HttpContext.Response.StatusCode = 200;
            return userDetails;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<UserDetails> Login(string userName,string password)
        {
            UserDetails userDetails = new UserDetails();
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {

            }
          // UserDetails userDetails =  await _userService.Login(userName, password);

            return userDetails;
        }
    }
}
