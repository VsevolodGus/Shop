using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
