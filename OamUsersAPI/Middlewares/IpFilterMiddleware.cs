﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OAM.Core.BAL.IService;
using OAM.Core.Helpers;
using OAM.Core.Models.Base_Models;
using System.Net;
using static OAM.Core.Enums.Enums;
using System.Configuration;

namespace OAM_API.Middlewares
{
    public class IpFilterMiddleware
    {
        //Declaration
        private readonly RequestDelegate requestDelegate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonService _commonService;

        //Constructor
        public IpFilterMiddleware(RequestDelegate request, IHttpContextAccessor contextAccessor, ICommonService commonService)
        {
            requestDelegate = request;
            _httpContextAccessor = contextAccessor;
            _commonService = commonService;
        }

        public Task Invoke(HttpContext context)
        {
            bool IsIpFilterEnabled = Utility.GetBool(_commonService.GetAppSettings("IpFilterEnabled"));
            if (IsIpFilterEnabled)
            {
                if (context != null)
                {
                    bool isValidIp = true;
                    string? IpAddress = context.Request.Headers["ipaddress"];
                    if (!string.IsNullOrWhiteSpace(IpAddress))
                    {
                        isValidIp = IsValidIpAddress(IpAddress);
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
                        apiBaseResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                        apiBaseResponse.Message = Utility.GetEnumDisplayName(StatusMessage.InvalidIP);
                        var jsonapiIpValidation = JsonConvert.SerializeObject(apiBaseResponse, Formatting.Indented);
                        context.Response.WriteAsync(jsonapiIpValidation);
                        Task<HttpContext> responseStatus = Task<HttpContext>.Factory.StartNew(() => context);
                        return responseStatus;
                    }
                }

            }
            return requestDelegate.Invoke(context);
        }

        #region To Check Whether IP Address is Valid or Not
        public bool IsValidIpAddress(string IpAddress)
        {

            return _commonService.IsValidIpAddress(IpAddress);
        }
        #endregion

    }
    public static class IpFilterMiddlewareExtensions
    {
        public static IApplicationBuilder UseIpFilter(this IApplicationBuilder app)
        {
            return app.UseMiddleware<IpFilterMiddleware>();
        }
    }
}
