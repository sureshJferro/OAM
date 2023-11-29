using Azure.Core;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OAM.Core.BAL.IService;
using OAM.Core.BAL.Service;
using OAM.Core.Models.Base_Models.API_Responses;
using System;
using ServiceUtility = OAM.Core.Helpers;
using System.Text;
using System.Net;

namespace OAM_API.Middlewares
{
    public class LogURLMiddleware
    {
        //Declaration
        private readonly RequestDelegate _next;
        private readonly ILogger<LogURLMiddleware> _logger;
        private readonly ICommonService _commonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Constructor
        public LogURLMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ICommonService commonService, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<LogURLMiddleware>() ??
            throw new ArgumentNullException(nameof(loggerFactory));
            _commonService = commonService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            ApiLogEntryResponse apiLogEntry = CreateApiLogEntryWithRequestData(context);
            long requestResponseId = _commonService.SaveApiRequestResposelog(apiLogEntry);
            context.Request.Headers.Add(ServiceUtility.Constants.APIRequestId, requestResponseId.ToString());
            var originalBodyStream = context.Response.Body;
            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;
                bool getWithRequestBody = false;

                if (context.Request.Method.ToUpper() == "GET" || context.Request.Method.ToUpper() == "DELETE")
                {
                    if (context.Request.Body.Length > 0)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        getWithRequestBody = true;
                    }
                }
                //Continue down the Middleware pipeline, eventually returning to this class
                await _next.Invoke(context);

                apiLogEntry.ResponseContentBody = FormatResponse(context.Response);

                #region Assigning HTTP Status code same as in response for Forms.
                //if (!string.IsNullOrWhiteSpace(apiLogEntry.ResponseContentBody) && apiLogEntry.ResponseContentBody.Contains("StatusCode"))
                //{
                //    context.Response.StatusCode = (int)JObject.Parse(apiLogEntry.ResponseContentBody)["StatusCode"];

                //}
                #endregion

                apiLogEntry.ResponseStatusCode = context.Response.StatusCode;
                apiLogEntry.ResponseContentType = context.Request.ContentType;
                apiLogEntry.ResponseTimestamp = DateTime.Now;
                apiLogEntry.ResponseHeaders = SerializeHeaders(context.Request.Headers);

                apiLogEntry.RequestIpAddress = !string.IsNullOrWhiteSpace((string)JObject.Parse(apiLogEntry.RequestHeaders)["X-Forwarded-For"]) ? JObject.Parse(apiLogEntry.RequestHeaders)["X-Forwarded-For"].ToString().Split(",").FirstOrDefault() : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                //apiLogEntry.RequestIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                //if (context.Response.StatusCode != (int)HttpStatusCode.OK)
                //{
                //    if (!string.IsNullOrWhiteSpace(apiLogEntry.ResponseContentBody))
                //    {
                //        apiLogEntry.ResponseErrorMsg = apiLogEntry.ResponseContentBody;
                //        apiLogEntry.ResponseContentBody = null;
                //    }
                //    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                //    {
                //        apiLogEntry.ResponseErrorMsg = HttpStatusCode.Unauthorized.ToString();
                //        apiLogEntry.ResponseContentBody = null;
                //    }
                //    if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError && string.IsNullOrWhiteSpace(apiLogEntry.ResponseContentBody))
                //    {
                //        apiLogEntry.ResponseErrorMsg = HttpStatusCode.InternalServerError.ToString();
                //        apiLogEntry.ResponseContentBody = null;
                //    }
                //    if (getWithRequestBody)
                //    {
                //        context.Response.Clear();
                //        apiLogEntry.ResponseErrorMsg = HttpStatusCode.BadRequest.ToString();
                //        apiLogEntry.ResponseStatusCode = (int)HttpStatusCode.BadRequest;
                //        apiLogEntry.ResponseContentBody = "HTTP GET/HTTP DELETE API Endpoints don't support request body.";
                //        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                //    }
                //    apiLogEntry.ApiStatusId = (int)HttpStatusCode.BadRequest;
                //}
                //else if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                //{
                //    apiLogEntry.ApiStatusId = (int)HttpStatusCode.OK;
                //}
                apiLogEntry.APIRequestId = ServiceUtility.Utility.GetLong(ServiceUtility.Utility.GetHttpRequestHeader(context.Request.Headers,
                        ServiceUtility.Constants.APIRequestId));
                if (apiLogEntry.APIRequestId > 0)
                {
                    _commonService.SaveApiRequestResposelog(apiLogEntry);
                    context.Request.Headers.Remove("RequestId");
                }

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        ApiLogEntryResponse CreateApiLogEntryWithRequestData(HttpContext httpContext)
        {
            var requestValue = httpContext.Request.Path.Value.ToUpper();
            var apiLogEntryResponse = new ApiLogEntryResponse();
            apiLogEntryResponse.Application = "[insert-calling-app-here]";
            apiLogEntryResponse.Machine = Environment.MachineName;
            apiLogEntryResponse.RequestContentType = httpContext.Request.ContentType;
            apiLogEntryResponse.RequestContentBody = FormatRequest(httpContext.Request);
            apiLogEntryResponse.RequestActionMethod = httpContext.Request.Path.Value;
            apiLogEntryResponse.RequestMethod = httpContext.Request.Method;
            apiLogEntryResponse.RequestHeaders = SerializeHeaders(httpContext.Request.Headers);
            apiLogEntryResponse.RequestIpAddress = !string.IsNullOrWhiteSpace((string)JObject.Parse(apiLogEntryResponse.RequestHeaders)["X-Forwarded-For"]) ? JObject.Parse(apiLogEntryResponse.RequestHeaders)["X-Forwarded-For"].ToString().Split(",").FirstOrDefault() : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            apiLogEntryResponse.RequestUri = httpContext.Request.Path.ToString() + (httpContext.Request.QueryString.HasValue ? httpContext.Request.QueryString.Value : string.Empty);
            apiLogEntryResponse.ControllerName = httpContext.Request.Path.Value.Split("/").Count() > 2 ? httpContext.Request.Path.Value.Split("/")[2].Substring(0, httpContext.Request.Path.Value.Split("/")[2].Length) : string.Empty;
            apiLogEntryResponse.ActionName = httpContext.Request.Path.Value.Split("/").Count() > 3 ? httpContext.Request.Path.Value.Split("/")[3] : string.Empty;
            return apiLogEntryResponse;
        }

        #region Format Response
        private string FormatRequest(HttpRequest request)
        {
            var bodyContent = string.Empty;
            var injectedRequestStream = new MemoryStream();
            using (var bodyReader = new StreamReader(request.Body))
            {
                var reqBodyTxt = bodyReader.ReadToEndAsync();
                if (reqBodyTxt != null)
                {
                    var bytesToWrite = Encoding.UTF8.GetBytes(reqBodyTxt.Result);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    request.Body = injectedRequestStream;
                    bodyContent = reqBodyTxt.Result;
                }
                if (string.IsNullOrWhiteSpace(bodyContent))
                {
                    bodyContent = string.Concat(request.Path.Value, request.QueryString.Value);
                }
            }
            return bodyContent;
        }
        #endregion

        #region Format Response
        private string FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);
            //...and copy it into a string
            string text = new StreamReader(response.Body).ReadToEnd();
            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
        #endregion

        #region Seralize Headers
        public string SerializeHeaders(IHeaderDictionary headers)
        {
            return JsonConvert.SerializeObject(headers);
        }
        #endregion
    }
    public static class LogURLMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogUrl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogURLMiddleware>();
        }
    }
}
