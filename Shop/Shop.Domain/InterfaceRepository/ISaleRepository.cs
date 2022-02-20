using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISaleRepository
    {
        Task<List<SaleDto>> GetSales(bool allUsers, Guid? userId, string search, Guid? SalePoinId, int skipCount, int count);

        Task<SaleDto> GetSaleByPKID(long saleId);

        Task AddSale(SaleDto model);

        Task UpdateSale(SaleDto model);

        Task RemoveProduct(long saleId, Guid productId, int count, bool fullProduct);

        //Task AddProudct(long saleId, Guid productId, int count);
    }
}
