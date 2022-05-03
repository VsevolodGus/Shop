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
        private readonly DbContextFactory dbContextFactory;

        public SaleRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<long> AddSale(SaleDto model)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));

            await dc.AddAsync(new SaleDto()
            {
                SalePointId = model.SalePointId,
                UserId = model.UserId,
                Date = model.Date
            });
            await dc.SaveChangesAsync();


            var sale = await dc.Sales.FirstOrDefaultAsync(c => c.IsChanled == false && c.SalePointId == model.SalePointId && c.Date == model.Date && c.UserId == model.UserId);
            var itemSale = model.SalesDatas.ToList();
            itemSale.ForEach(c => c.SaleId = sale.PKID);
            await dc.SalesDatas.AddRangeAsync(itemSale);
            await dc.SaveChangesAsync();
            return sale.PKID;
        }

        public async Task<SaleDto> GetSaleByPKID(long saleId)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));

            return await dc.Sales.Where(c => c.IsChanled == false && c.PKID == saleId)
                           .FirstOrDefaultAsync();
        }

        public async Task<List<SaleDto>> GetSales(bool allUsers, Guid? userId, string search, Guid? salePoinId, int skipCount, int count)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));

            var query = dc.Sales.Where(c => c.IsChanled == false);

            #region Фильтрация по юзеру 
            if (!allUsers)
            {
                if (userId.HasValue)
                {
                    query = query.Where(c => c.UserId == userId.Value);
                }
                else
                {
                    query = query.Where(c => c.UserId == null);
                }
            }
            #endregion

            if (salePoinId.HasValue)
            {
                query = query.Where(c => c.SalePointId == salePoinId.Value);
            }


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.SalePoint.Address.Contains(search)
                                         || c.SalePoint.Name.Contains(search)
                                         || (c.UserId != null && c.User.Name.Contains(search)));
            }


            return await query.OrderByDescending(c => c.Date)
                             .Skip(skipCount).Take(count)
                             .ToListAsync();
        }


        public async Task UpdateSale(SaleDto saleDto)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));

            if (!await dc.Sales.AnyAsync(c => c.IsChanled == false && c.PKID == saleDto.PKID))
                return;


            var sale = await dc.Sales.FirstOrDefaultAsync(c => c.IsChanled == false && c.PKID == saleDto.PKID);
            sale.SalesDatas = saleDto.SalesDatas;
            sale.Date = DateTime.UtcNow;
        }


        public async Task RemoveProduct(long saleId, Guid productId, int count, bool fullProduct)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));
            if (!await dc.Sales.AnyAsync(c => c.IsChanled == false && c.PKID == saleId))
                return;

            var saleProducts = await dc.Sales.Where(c => c.IsChanled == false && c.PKID == saleId)
                                             .Select(c => c.SalesDatas.FirstOrDefault(v => v.ProductId == productId))
                                             .FirstOrDefaultAsync();

            if (saleProducts.ProductQuantity < count || fullProduct)
            {
                dc.Remove(saleProducts);
            }
            else
            {
                saleProducts.ProductQuantity -= count;
            }

            await dc.SaveChangesAsync();

        }
    }
}
