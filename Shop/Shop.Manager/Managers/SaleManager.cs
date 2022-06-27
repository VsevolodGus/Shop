using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shop.Domain.DTO;
using Shop.Domain.Model;
using Shop.Manager.Models;
using Shop.Domain.InterfaceRepository;
using Shop.Manager.Factories;
using Shop.Domain;

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

        public async Task<long> CreateSale(Guid? userId, Guid salePointId, string comment)
        {
            var saleDto = Factory.CreateSale(userId, salePointId, comment, new List<SalesDataDto>());

            return await _saleRepository.AddSale(saleDto);
        }

        public async Task AddProductInSale(OperationProductInSale model, Guid userId)
        {
            var sale = await GetSaleByAccessAsync(model.SaleId, userId);
            if (sale is null)
                return;

            var product = await _productRepository.GetByIdAsync(model.ProductId);
            sale.FillSale(model.ProductId, model.Count, product.Price);
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));
        }


        public async Task<bool> RemoveProductFromSale(long saleId, Guid productId, long count, Guid userId, bool fullProduct = false)
        {
            var sale = await GetSaleByAccessAsync(saleId, userId);
            if (sale is null)
                return false;


            sale.RemoveItem(productId, count, fullProduct);
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));

            return true;
        }


        public async Task CancelSale(long saleId, Guid userId)
        {
            var sale = await GetSaleByAccessAsync(saleId, userId);
            if (sale is null)
                return;


            sale.IsCancel = true;
            await _saleRepository.UpdateAsync(Sale.Mapper.Map(sale));
        }

        private async Task<Sale> GetSaleByAccessAsync(long saleId, Guid userId)
        {
            var saleEntity = await _saleRepository.GetByIdAsync(saleId);
            if (IsNotAccessToSale(saleEntity, userId))
                return null;

            return Sale.Mapper.Map(saleEntity);
        }

        private bool IsNotAccessToSale(in SaleEntity sale, in Guid userId)
        {
            return sale is null 
                   && ( (sale.UserId.HasValue && userId == Guid.Empty) 
                        || sale.UserId.Value == userId);
        }
    }
}
