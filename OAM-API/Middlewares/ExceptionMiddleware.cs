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
        ILogger<ExceptionMiddleware> _logger;

        //Constructor
        public ExceptionMiddleware(RequestDelegate request, IHttpContextAccessor contextAccessor, ICommonService commonService, ILogger<ExceptionMiddleware> logger)
        {
            requestDelegate = request;
            _httpContextAccessor = contextAccessor;
            _commonService = commonService;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(500, ex, "Exception in OAMAPI");
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
