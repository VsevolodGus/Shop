using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Memory.Repository
{
    internal class ProductRepository : IProductRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public ProductRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<ProductEntity> GetByIdAsync(Guid productId)
        {
            var dc = dbContextFactory.Create(typeof(ProductRepository));

            return await dc.Products.FirstOrDefaultAsync(c => c.ProductId == productId && c.IsDeleted == false);
        }

        public async Task<Guid> AddProduct(ProductEntity product)
        {
            var dc = dbContextFactory.Create(typeof(ProductRepository));
            await dc.Products.AddAsync(product);
            await dc.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<Guid> UpdateProduct(ProductEntity model)
        {
            var dc = dbContextFactory.Create(typeof(ProductRepository));

            //bool IsExsistsProduct = await dc.Products.AnyAsync(c => c.IsDeleted == false && c.ProductId == model.ProductId);
            //if (!IsExsistsProduct)
            //    return Guid.Empty;

            dc.Products.Update(model);
            await dc.SaveChangesAsync();
            return model.ProductId;
        }


        public async Task<List<ProductEntity>> GetProductsBySalePoint(Guid salePointId, string search, int skipCount, int count)
        {
            var dc = dbContextFactory.Create(typeof(ProductRepository));

            var queryProductList = dc.SalePoints.Where(c => c.Id == salePointId)
                                                .SelectMany(v => v.ProvidedProducts.Where(c => c.Count > 0)
                                                                                   .Select(c => c.Product));
            queryProductList = GetQuerySearchNameAndOrderSkipCount(queryProductList, search, skipCount, count);

            return await queryProductList.ToListAsync();
        }

        public async Task<List<ProductEntity>> GetProductByTitleOrDescription(string search, int skipCount, int count)
        {
            var dc = dbContextFactory.Create(typeof(ProductRepository));
            
            var queryProductList = dc.Products.Where(c => c.IsDeleted == false);
            queryProductList = GetQuerySearchNameAndOrderSkipCount(queryProductList, search, skipCount, count);

            return await queryProductList.ToListAsync();
        }

        // переимонвать
        private IQueryable<ProductEntity> GetQuerySearchNameAndOrderSkipCount(IQueryable<ProductEntity> query, string search, int skipCount, int count)
        {
            if (!string.IsNullOrEmpty(search))
                query = query.Where(c => c.Name.Contains(search));

            return query.OrderBy(c => c.Price)
                        .Skip(skipCount).Take(count);
        }

      
    }
}
