using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OAM.Core.BAL.IService;
using OAM.Core.BAL.Service;
using OAM.Core.DAL.IRepository;
using OAM.Core.DAL.Repository;
using OAM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.Resolver
{
    public static class Resolver
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<ICommonService, CommonService>();

            // Register your repositories here
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<ICommonRepository, CommonRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
