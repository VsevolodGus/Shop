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
        private readonly DbContextFactory dbContextFactory;

        public SalePointRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }



        public async Task<Guid> AddSalePoint(SalePointDto model)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));
            await dc.SalePoints.AddAsync(model);
            await dc.SaveChangesAsync();
            return model.Id;
        }

        public async Task<Guid> UpdateSalePoint(SalePointDto model)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));
            dc.SalePoints.Update(model);
            await dc.SaveChangesAsync();
            return model.Id;
        }

        public async Task AddProductInAssortment(Guid? salePointId, Dictionary<Guid, long> products)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));

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


        public async Task<SalePointDto> GetSalePointById(Guid salePointId)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));

            return await dc.SalePoints.Where(c => c.Id == salePointId).FirstOrDefaultAsync();
        }
        public async Task<List<SalePointDto>> GetSalePointBySearch(string search, int skipCount, int count)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));

            if (string.IsNullOrEmpty(search))
            {
                return await dc.SalePoints.OrderBy(c => c.ProvidedProducts.Count)
                                      .Skip(skipCount).Take(count)
                                      .ToListAsync();
            }
            else
            {
                return await dc.SalePoints.Where(c=> c.Name.Contains(search) || c.Address.Contains(search))
                                          .OrderBy(c => c.ProvidedProducts.Count)
                                          .Skip(skipCount).Take(count)
                                          .ToListAsync();
            }
            
        }

        public async Task<List<SalePointDto>> GetSalePointByHasProduct(Guid prodcutId, int skipCount, int count, long? needCount = 1)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));

            var query = dc.SalePoints.Where(c => c.ProvidedProducts.Any(c => c.ProductId == prodcutId));

            if (needCount.HasValue)
            {
                query = query.Where(c => c.ProvidedProducts.Any(c => c.ProductId == prodcutId && c.Count == needCount.Value));
            }

            return await query.OrderBy(c => c.ProvidedProducts.Count)
                              .Skip(skipCount).Take(count)
                              .ToListAsync();
        }


        public async Task DeleteEmptyProductFromSalePoints(Guid? salePointId)
        {
            var dc = dbContextFactory.Create(typeof(SalePointRepository));

            var notExsistsProductInSalePoint = await dc.SalePoints.Where(c => salePointId.HasValue || c.Id == salePointId.Value)
                                                                      .SelectMany(c => c.ProvidedProducts.Where(c => c.Count < 1))
                                                                      .ToListAsync();

            dc.ProvidedProducts.RemoveRange(notExsistsProductInSalePoint);
            await dc.SaveChangesAsync();

        }


    }
}
