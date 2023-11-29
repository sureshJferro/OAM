using OAM.Core.BAL.IService;
using ServiceUtility = OAM.Core.Helpers;

namespace OAM_API.Middlewares
{
    public class ExceptionMiddleware
    {
        //Declaration
        private readonly RequestDelegate requestDelegate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonService _commonService;

        //Constructor
        public ExceptionMiddleware(RequestDelegate request, IHttpContextAccessor contextAccessor, ICommonService commonService)
        {
            requestDelegate = request;
            _httpContextAccessor = contextAccessor;
            _commonService = commonService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await requestDelegate(context);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        public void HandleException(Exception ex)
        {
            
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
