using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Shop.Memory
{
    public class DbContextFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public DbContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ShopDbContext Create(Type repositoryType)
        {
            var services = httpContextAccessor.HttpContext.RequestServices;

            var dbContexts = services.GetService<Dictionary<Type, ShopDbContext>>();
            if (!dbContexts.ContainsKey(repositoryType))
                dbContexts[repositoryType] = services.GetService<ShopDbContext>();

            return dbContexts[repositoryType];
        }
    }
}
