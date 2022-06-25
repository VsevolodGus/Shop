using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISaleRepository
    {
        // разделить на две функции
        Task<List<SaleEntity>> GetSales(bool allUsers, Guid? userId, string search, Guid? SalePoinId, int skipCount, int count);

        Task<SaleEntity> GetByIdAsync(long saleId);

        Task<long> AddSale(SaleEntity model);

        Task UpdateAsync(SaleEntity model);

        Task RemoveProductAsync(long saleId, Guid productId, int count, bool fullProduct);
    }
}
