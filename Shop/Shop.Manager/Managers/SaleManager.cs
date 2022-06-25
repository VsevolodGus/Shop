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

        public async Task<List<SaleEntity>> GetSales(SalesFilter filter, int skipCount, int count)
        {
            return await _saleRepository.GetSales(filter.AllUser, filter.UserId, filter.Search, filter.SalePointId, skipCount, count);
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

        public async Task AddProductInSale(long saleId, Guid productId, long? count, Guid userId)
        {
            var saledto = await _saleRepository.GetByIdAsync(saleId);
            // дублируется в нескольких местах, можно вынести
            if (saledto is null && ((saledto.UserId.HasValue && userId == Guid.Empty) || saledto.UserId.Value == userId))
                return;

            var sale = Sale.Mapper.Map(saledto);
            var product = await _productRepository.GetByIdAsync(productId);
            sale.FillSale(productId, count.Value, product.Price);
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));
            return;
        }


        public async Task<bool> RemoveProductFromSale(long saleId, Guid productId, long count, Guid userId, bool fullProduct = false)
        {
            var saledto = await _saleRepository.GetByIdAsync(saleId);
            // дублируется в нескольких местах, можно вынести
            if (saledto is null && ((saledto.UserId.HasValue && userId == Guid.Empty) || saledto.UserId.Value == userId))
                return false;

            var sale = Sale.Mapper.Map(saledto);
            sale.RemoveItem(productId, count, fullProduct);
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));

            return true;
        }



        public async Task CancelSale(long saleId, Guid userId)
        {
            var saledto = await _saleRepository.GetByIdAsync(saleId);
            // дублируется в нескольких местах, можно вынести
            if (saledto is null && ((saledto.UserId.HasValue && userId == Guid.Empty) || saledto.UserId.Value == userId))
                return;

            var sale = Sale.Mapper.Map(saledto);
            sale.IsCancel = true;
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));
            return;
        }
    }
}
