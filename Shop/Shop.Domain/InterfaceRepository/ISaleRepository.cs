using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISaleRepository
    {
        Task<List<SaleEntity>> GetSalesForAllUsers(Guid? salePointId, string search, int skipCount, int count);
        Task<List<SaleEntity>> GetSales(Guid userId, Guid? salePointId, string search, int skipCount, int count);
        Task<List<SaleEntity>> GetSalesNoRegirteredUsers(Guid? salePointId, string search, int skipCount, int count);

        Task<SaleEntity> GetByIdAsync(long saleId);

        Task<long> AddSale(SaleEntity model);

        Task UpdateAsync(SaleEntity model);

        Task RemoveProductAsync(long saleId, Guid productId, int count, bool fullProduct);
    }
}
