using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using Shop.Domain;

namespace Shop.Manager
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<Guid> SetProductAsync(ProductEntity product)
        {
            var productExists = await _productRepository.GetByIdAsync(product.ProductId);

            if (productExists is null || product.ProductId == default)
            {
                return await _productRepository.AddProduct(product);
            }
            else
            {
                return await _productRepository.UpdateProduct(product);
            }
        }



        public async Task<List<ProductEntity>> GetProductBySalePoint(Guid salePointId, BaseFilter filter)
        {
            return await _productRepository.GetProductsBySalePoint(salePointId, filter.Search, filter.SkipCount, filter.Count);
        }

        public async Task<List<ProductEntity>> GetProductBySearch(BaseFilter filter)
        {
            return await _productRepository.GetProductByTitleOrDescription(filter.Search, filter.SkipCount, filter.Count);
        }

        public async Task<ProductEntity> GetProductById(Guid productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }
    }
}
