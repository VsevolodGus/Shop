using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public SaleRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<SaleDto> GetsaleByPKID(long saleId)
        {
            using (var dc = dbContextFactory.Create(typeof(UserRepository)))
            {
                return await dc.Sales.Where(c => c.IsChanled == false && c.PKID == saleId)
                               .FirstOrDefaultAsync();
            }
        }

        public async Task<List<SaleDto>> GetSaleByUser(Guid? userId, Guid? salePoint, string search, int skipCount, int count)
        {
            using (var dc = dbContextFactory.Create(typeof(UserRepository)))
            {
                var query = dc.Sales.Where(c => c.IsChanled == false);

                #region Фильтрация по юзеру 
                if (userId.HasValue)
                {
                    query = query.Where(c => c.UserId == userId.Value);
                }
                else
                {
                    query = query.Where(c => c.UserId == null);
                }
                #endregion

                if (salePoint.HasValue)
                {
                    query = query.Where(c => c.SalePointId == salePoint.Value);
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



        }
    }
}
