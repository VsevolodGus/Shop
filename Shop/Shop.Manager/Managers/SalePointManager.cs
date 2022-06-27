using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shop.Domain.DTO;
using Shop.Manager.Models;
using Shop.Domain.InterfaceRepository;
using Shop.Domain;

namespace Shop.Manager
{
    public class SalePointManager
    {
        private readonly ISalePointRepository _salePointRepository;

        public SalePointManager(ISalePointRepository salePointRepository)
        {
            _salePointRepository = salePointRepository;
        }


        public async Task<Guid> SetSalePoint(SalePointEntity model)
        {
            var productExists = await _salePointRepository.GetByIdAsync(model.Id);
            try
            {
                if (productExists is null || model.Id == default)
                {
                    return await _salePointRepository.AddAsync(model);
                }
                else
                {
                    return await _salePointRepository.UpdateAsync(model);
                }
            }
            catch
            {
                return Guid.Empty;
            }
        }
        public async Task AddProductInAssortment(SettingsAddingProducts settingsAddingProducts)
        {
            await _salePointRepository.AddProductIntoAssortmentAsync(settingsAddingProducts.SalePointId, settingsAddingProducts.Products);
        }

        public async Task<bool> DeleteAbsenceProductFromSalePoint(Guid? salePointId)
        {
            try
            {
                await _salePointRepository.DeleteEmptyProductФынтс(salePointId);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<SalePointEntity>> GetSalePointBySearch(BaseFilter filter)
        {
            return await _salePointRepository.GetListAsync(filter.Search, filter.SkipCount, filter.Count);
        }
        public async Task<SalePointEntity> GetSalePointById(Guid salePointId)
        {
            return await _salePointRepository.GetByIdAsync(salePointId);
        }
        public async Task<List<SalePointEntity>> GetListSalePointWhereHasProductById(Guid productId, BaseFilter filter)
        {
            return await _salePointRepository.GetByHasProductCountAsync(productId, filter.SkipCount, filter.Count);
        }




    }
}
