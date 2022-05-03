using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;

namespace Shop.Manager
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }


        public async Task<Guid> SetProduct(ProductEntity product)
        {
            var productExists = await _productRepository.GetProductById(product.ProductId);

            if (productExists is null || product.ProductId == default)
            {
                return await _productRepository.AddProduct(product);
            }
            else
            {
                return await _productRepository.UpdateProduct(product);
            }

        }



        public async Task<List<ProductEntity>> GetProductBySalePoint(Guid salePointId, string search, int skipCount, int count)
        {
            return await _productRepository.GetProductsBySalePoint(salePointId, search, skipCount, count);
        }

        public async Task<List<ProductEntity>> GetProductBySearch(string search, int skipCount, int count)
        {
            return await _productRepository.GetProductByTitleOrDescription(search, skipCount, count);
        }

        public async Task<ProductEntity> GetProductById(Guid productId)
        {
            return await _productRepository.GetProductById(productId);
        }
    }
}
