using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISalePointRepository
    {

        Task<SalePointEntity> GetSalePointById(Guid salePointId);

        Task<List<SalePointEntity>> GetSalePointBySearch(string search, int skipCount, int count);

        Task<List<SalePointEntity>> GetSalePointByHasProduct(Guid prodcutId, int skipCount, int count, long? needCount = 1);

        Task DeleteEmptyProductFromSalePoints(Guid? salePointId);

        Task<Guid> AddSalePoint(SalePointEntity model);

        Task<Guid> UpdateSalePoint(SalePointEntity model);

        Task AddProductInAssortment(Guid? salePointId, Dictionary<Guid, long> products);
    }
}
