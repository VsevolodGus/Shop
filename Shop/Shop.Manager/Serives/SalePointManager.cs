using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using Shop.Manager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Manager
{
    public class SalePointManager
    {
        private readonly ISalePointRepository _salePointRepository;

        public SalePointManager(ISalePointRepository salePointRepository)
        {
            this._salePointRepository = salePointRepository;
        }


        public async Task<bool> SetSalePoint(SalePointDto model)
        {
            var productExists = await _salePointRepository.GetSalePointById(model.Id);
            try
            {
                if (productExists is null)
                {
                    await _salePointRepository.AddSalePoint(model);
                }
                else
                {
                    await _salePointRepository.UpdateSalePoint(model);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task AddProductInAssortment(SettingsAddingProducts settingsAddingProducts)
        {
            await _salePointRepository.AddProductInAssortment(settingsAddingProducts.SalePointId, settingsAddingProducts.Products);
        }

        public async Task<bool> DeleteAbsenceProductFromSalePoint(Guid? salePointId)
        {
            try
            {
                await _salePointRepository.DeleteEmptyProductFromSalePoints(salePointId);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<SalePointDto>> GetSalePointBySearch(string search, int skipCount, int count)
        {
            return await _salePointRepository.GetSalePointBySearch(search, skipCount, count);
        }
        public async Task<SalePointDto> GetSalePointById(Guid salePointId)
        {
            return await _salePointRepository.GetSalePointById(salePointId);
        }
        public async Task<List<SalePointDto>> GetListSalePointWhereHasProductById(Guid productId, int skipCount, int count)
        {
            return await _salePointRepository.GetSalePointByHasProduct(productId, skipCount, count);
        }




    }
}
