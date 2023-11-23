using Microsoft.Extensions.DependencyInjection;
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
            // Register your services and repositories here
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            // Add more registrations as needed

            return services;
        }
    }
}
