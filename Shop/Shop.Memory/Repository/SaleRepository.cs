using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Memory.Repository
{
    internal class SaleRepository : ISaleRepository
    {
        private readonly DbContextFactory _dbContextFactory;

        public SaleRepository(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<long> AddSale(SaleEntity model)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));
            await dc.AddAsync(model);
            await dc.SaveChangesAsync();
            return model.PKID;
        }

        public async Task UpdateAsync(SaleEntity saleDto)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));
            dc.Sales.Update(saleDto);
            await dc.SaveChangesAsync();
        }



        public async Task<SaleEntity> GetByIdAsync(long saleId)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));

            return await dc.Sales.Where(c => c.IsChanled == false && c.PKID == saleId)
                           .FirstOrDefaultAsync();
        }

        public async Task<List<SaleEntity>> GetSalesForAllUsers(Guid? salePointId, string search, int skipCount, int count)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));

            var query = dc.Sales.Where(c => c.IsChanled == false);
            query = GetFilterBySalePointAndSearchStringAndOrdered(query, salePointId, search, skipCount, count);

            return await query.ToListAsync();
        }

        public async Task<List<SaleEntity>> GetSales(Guid userId, Guid? salePointId, string search, int skipCount, int count)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));

            var query = dc.Sales.Where(c => c.IsChanled == false
                                            && c.UserId == userId);
            query = GetFilterBySalePointAndSearchStringAndOrdered(query, salePointId, search, skipCount, count);

            return await query.ToListAsync();
        }

        public async Task<List<SaleEntity>> GetSalesNoRegirteredUsers(Guid? salePointId, string search, int skipCount, int count)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));

            var query = dc.Sales.Where(c => c.IsChanled == false
                                            && !c.UserId.HasValue);
            query = GetFilterBySalePointAndSearchStringAndOrdered(query, salePointId, search, skipCount, count);

            return await query.ToListAsync();
        }

        private IQueryable<SaleEntity> GetFilterBySalePointAndSearchStringAndOrdered(IQueryable<SaleEntity> query, Guid? salePointId, string search, int skipCount, int count)
        {
            if (salePointId.HasValue)
            {
                query = query.Where(c => c.SalePointId == salePointId.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.SalePoint.Address.Contains(search)
                                         || c.SalePoint.Name.Contains(search)
                                         || c.User.Name.Contains(search));
            }

            query = query.OrderByDescending(c => c.Date)
                         .Skip(skipCount).Take(count);

            return query;
        }
        
        
        
        public async Task RemoveProductAsync(long saleId, Guid productId, int count, bool fullProduct)
        {
            var dc = _dbContextFactory.Create(typeof(UserRepository));

            var saleProducts = await dc.Sales.Where(c => c.IsChanled == false && c.PKID == saleId)
                                             .Select(c => c.SalesDatas.FirstOrDefault(v => v.ProductId == productId))
                                             .FirstOrDefaultAsync();

            //вынести на уровень менеджера
            if (saleProducts.ProductQuantity < count || fullProduct)
                dc.Remove(saleProducts);
            else
                saleProducts.ProductQuantity -= count;

            await dc.SaveChangesAsync();
        }

        
    }
}
