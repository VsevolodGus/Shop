using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISaleRepository
    {
        Task<List<SaleEntity>> GetSales(bool allUsers, Guid? userId, string search, Guid? SalePoinId, int skipCount, int count);

        Task<SaleEntity> GetSaleByPKID(long saleId);

        Task<long> AddSale(SaleEntity model);

        Task UpdateSale(SaleEntity model);

        Task RemoveProduct(long saleId, Guid productId, int count, bool fullProduct);
    }
}
