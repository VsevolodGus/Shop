using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manager
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddManagers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ProductManager>();
            services.AddSingleton<SaleManager>();
            services.AddSingleton<SalePointManager>();
            services.AddSingleton<UserManager>();

            return services;
        }
    }
}
