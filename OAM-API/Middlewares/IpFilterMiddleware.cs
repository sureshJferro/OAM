using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using OAM.Core.BAL.IService;
using OAM.Core.Helpers;
using OAM.Core.Models.Base_Models;
using System.Net;
using static OAM.Core.Enums.Enums;

namespace OAM_API.Middlewares
{
    public class IpFilterMiddleware
    {
        //Declaration
        private readonly RequestDelegate requestDelegate;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Constructor
        public IpFilterMiddleware(RequestDelegate request,IHttpContextAccessor contextAccessor) {
        requestDelegate = request;
        _httpContextAccessor = contextAccessor;
        }
         
        public Task Invoke(HttpContext context)
        {
            if (context!=null)
            {
                bool isValidIp = true;
                string? IpAddress = context.Request.Headers["ipaddress"];
                if (!string.IsNullOrWhiteSpace(IpAddress))
                {
                    isValidIp= IsValidIpAddress(IpAddress);
                    if (!isValidIp)
                    {
                        ApiBaseResponse apiBaseResponse = new ApiBaseResponse();
                        apiBaseResponse.Status = HttpStatusCode.Unauthorized.ToString();
                        apiBaseResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                        apiBaseResponse.Message = Utility.GetEnumDisplayName(StatusMessage.InvalidIP);
                        var jsonapiIpValidation = JsonConvert.SerializeObject(apiBaseResponse, Formatting.Indented);
                        context.Response.WriteAsync(jsonapiIpValidation);
                        Task<HttpContext> responseStatus = Task<HttpContext>.Factory.StartNew(() => context);
                        return responseStatus;
                    }
                }
                else
                {
                    ApiBaseResponse apiBaseResponse = new ApiBaseResponse();
                    apiBaseResponse.Status = HttpStatusCode.Unauthorized.ToString();
                    apiBaseResponse.StatusCode=(int)HttpStatusCode.Unauthorized;
                    apiBaseResponse.Message = Utility.GetEnumDisplayName(StatusMessage.InvalidIP);
                    var jsonapiIpValidation = JsonConvert.SerializeObject(apiBaseResponse, Formatting.Indented);
                    context.Response.WriteAsync(jsonapiIpValidation);
                    Task<HttpContext> responseStatus = Task<HttpContext>.Factory.StartNew(() => context);
                    return responseStatus;
                }

            }
            return requestDelegate.Invoke(context);
        }
        public bool IsValidIpAddress(string IpAddress)
        {
            var services = _httpContextAccessor.HttpContext.RequestServices;
            var commonService = (ICommonService)services.GetService(typeof(ICommonService));
            return commonService.IsValidIpAddress(IpAddress);
        }

    }
}
