using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.InterfaceRepository;
using Shop.Memory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                },
                ServiceLifetime.Transient
            );

            //для доступа к базе
            services.AddScoped<Dictionary<Type, ShopDbContext>>();
            services.AddSingleton<DbContextFactory>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();


            return services;
        }
    }
}

