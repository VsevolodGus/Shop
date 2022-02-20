using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.InterfaceRepository;
using Shop.Memory.Repository;
using System;
using System.Collections.Generic;

namespace Shop.Memory
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddEfRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Shop"));
                },
                ServiceLifetime.Transient
            );

            //для доступа к базе
            services.AddScoped<Dictionary<Type, ShopDbContext>>();
            services.AddSingleton<DbContextFactory>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ISalePointRepository, SalePointRepository>();
            services.AddSingleton<ISaleRepository, SaleRepository>();

            return services;
        }
    }
}

