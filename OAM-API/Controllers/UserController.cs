using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OAM.Core.BAL.IService;
using OAM.Core.Entities;
using OAM.Core.Models.Base_Models;
using OAM.Core.Models.Base_Models.API_Requests;

namespace OAM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ApiBaseResponse> Register(RegisterRequest registerRequest)
        {
            ApiBaseResponse apiBaseResponse = new ApiBaseResponse();
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            else
            {
                apiBaseResponse=await _userService.Register(registerRequest);
            }
            return apiBaseResponse;
        }
    }
}
