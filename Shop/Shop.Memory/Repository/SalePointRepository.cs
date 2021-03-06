using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Memory.Repository
{
    internal class SalePointRepository : ISalePointRepository
    {
        private readonly DbContextFactory _dbContextFactory;

        public SalePointRepository(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }



        public async Task<Guid> AddAsync(SalePointEntity model)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));
            await dc.SalePoints.AddAsync(model);
            await dc.SaveChangesAsync();
            return model.Id;
        }

        public async Task<Guid> UpdateAsync(SalePointEntity model)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));
            dc.SalePoints.Update(model);
            await dc.SaveChangesAsync();
            return model.Id;
        }


        // разбить
        public async Task AddProductIntoAssortmentAsync(Guid? salePointId, Dictionary<Guid, long> products)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));

            var allSalePoints = await dc.SalePoints.Select(c => c.Id).ToListAsync();

            foreach (var item in products)
            {
                var newProductInSalePoint = new ProvidedProductDto()
                {
                    ProductId = item.Key,
                    Count = item.Value,
                };

                if (salePointId.HasValue)
                {
                    newProductInSalePoint.SalePointId = salePointId.Value;
                    await dc.AddAsync(newProductInSalePoint);
                }
                else
                {
                    var newProducts = allSalePoints.Select(c => newProductInSalePoint.SalePointId = c);
                    await dc.AddRangeAsync(newProducts);
                }

                await dc.SaveChangesAsync();
            }
        }


        public async Task<SalePointEntity> GetByIdAsync(Guid salePointId)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));

            return await dc.SalePoints.Where(c => c.Id == salePointId).FirstOrDefaultAsync();
        }
        public async Task<List<SalePointEntity>> GetListAsync(string search, int skipCount, int count)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));

            var query = dc.SalePoints.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search) || c.Address.Contains(search));
            }

            return await dc.SalePoints.OrderBy(c => c.ProvidedProducts.Count)
                                      .Skip(skipCount).Take(count)
                                      .ToListAsync();
        }

        public async Task<List<SalePointEntity>> GetByHasProductCountAsync(Guid prodcutId, int skipCount, int count, long? needCount = 1)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));

            var query = dc.SalePoints.Where(c => c.ProvidedProducts.Any(c => c.ProductId == prodcutId));

            if (needCount.HasValue)
            {
                query = query.Where(c => c.ProvidedProducts.Any(c => c.ProductId == prodcutId && c.Count == needCount.Value));
            }

            return await query.OrderBy(c => c.ProvidedProducts.Count)
                              .Skip(skipCount).Take(count)
                              .ToListAsync();
        }


        public async Task DeleteEmptyProductФынтс(Guid? salePointId)
        {
            var dc = _dbContextFactory.Create(typeof(SalePointRepository));

            var notExsistsProductInSalePoint = await dc.SalePoints.Where(c => salePointId.HasValue || c.Id == salePointId.Value)
                                                                      .SelectMany(c => c.ProvidedProducts.Where(c => c.Count < 1))
                                                                      .ToListAsync();

            dc.ProvidedProducts.RemoveRange(notExsistsProductInSalePoint);
            await dc.SaveChangesAsync();

        }


    }
}
