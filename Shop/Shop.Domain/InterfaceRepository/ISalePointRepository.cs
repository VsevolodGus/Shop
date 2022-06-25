using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISalePointRepository
    {
        Task<SalePointEntity> GetByIdAsync(Guid salePointId);

        Task<List<SalePointEntity>> GetListAsync(string search, int skipCount, int count);

        Task<List<SalePointEntity>> GetByHasProductCountAsync(Guid prodcutId, int skipCount, int count, long? needCount = 1);

        Task DeleteEmptyProductФынтс(Guid? salePointId);

        Task<Guid> AddAsync(SalePointEntity model);

        Task<Guid> UpdateAsync(SalePointEntity model);

        Task AddProductIntoAssortmentAsync(Guid? salePointId, Dictionary<Guid, long> products);
    }
}
