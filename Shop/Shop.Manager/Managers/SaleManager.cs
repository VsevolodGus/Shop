using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shop.Domain.DTO;
using Shop.Domain.Model;
using Shop.Manager.Models;
using Shop.Domain.InterfaceRepository;

namespace Shop.Manager
{
    public class SaleManager
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;

        public SaleManager(ISaleRepository saleRepository, IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task<List<SaleEntity>> GetSales(SalesFilter filter)
        {
            if (filter.AllUser)
                return await _saleRepository.GetSalesForAllUsers(filter.SalePointId, filter.Search, filter.SkipCount, filter.Count);
            else
            {
                if (filter.UserId.HasValue)
                    return await _saleRepository.GetSales(filter.UserId.Value, filter.SalePointId, filter.Search, filter.SkipCount, filter.Count);
                else
                    return await _saleRepository.GetSalesNoRegirteredUsers(filter.SalePointId, filter.Search, filter.SkipCount, filter.Count);
            }
        }

        public async Task<long> CreateSale(Guid? userId, Guid salePointId)
        {
            var saleDto = new SaleEntity
            {
                Date = DateTime.UtcNow,
                UserId = userId,
                SalePointId = salePointId,
                SalesDatas = new List<SalesDataDto>(),
            };

            return await _saleRepository.AddSale(saleDto);
            
        }

        public async Task AddProductInSale(long saleId, Guid productId, long count, Guid userId)
        {
            // дублируется в нескольких местах, можно вынести
            var saleEntity = await _saleRepository.GetByIdAsync(saleId);
            if (IsAccessToSale(saleEntity, userId))
                return;
            var sale = Sale.Mapper.Map(saleEntity);

            var product = await _productRepository.GetByIdAsync(productId);
            sale.FillSale(productId, count, product.Price);
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));
        }


        public async Task<bool> RemoveProductFromSale(long saleId, Guid productId, long count, Guid userId, bool fullProduct = false)
        {
            // дублируется в нескольких местах, можно вынести
            var saleEntity = await _saleRepository.GetByIdAsync(saleId);
            if (IsAccessToSale(saleEntity, userId))
                return false;
            var sale = Sale.Mapper.Map(saleEntity);


            sale.RemoveItem(productId, count, fullProduct);
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));

            return true;
        }


        public async Task CancelSale(long saleId, Guid userId)
        {
            // дублируется в нескольких местах, можно вынести
            var saleEntity = await _saleRepository.GetByIdAsync(saleId);
            if (IsAccessToSale(saleEntity, userId))
                return;
            var sale = Sale.Mapper.Map(saleEntity);


            sale.IsCancel = true;
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));
        }

        private bool IsAccessToSale(SaleEntity sale, Guid userId)
        {
            return sale is null 
                   && ( (sale.UserId.HasValue && userId == Guid.Empty) 
                        || sale.UserId.Value == userId);
        }
    }
}
