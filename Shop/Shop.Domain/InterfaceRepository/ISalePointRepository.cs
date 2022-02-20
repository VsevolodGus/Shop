using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISalePointRepository
    {

        Task<SalePointDto> GetSalePointById(Guid salePointId);

        Task<List<SalePointDto>> GetSalePointBySearch(string search, int skipCount, int count);

        Task<List<SalePointDto>> GetSalePointByHasProduct(Guid prodcutId, int skipCount, int count, long? needCount = 1);

        Task DeleteEmptyProductFromSalePoints(Guid? salePointId);

        Task AddSalePoint(SalePointDto model);

        Task UpdateSalePoint(SalePointDto model);

        Task AddProductInAssortment(Guid? salePointId, Dictionary<Guid, long> products);
    }
}
