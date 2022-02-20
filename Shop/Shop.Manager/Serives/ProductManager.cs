using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Manager
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }


        public async Task<bool> SetProduct(ProductDto product)
        {
            var productExists = await _productRepository.GetProductById(product.ProductId);

            if (productExists is null)
            {
                return await _productRepository.AddProduct(product);
            }
            else
            {
                return await _productRepository.UpdateProduct(product);
            }

        }



        public async Task<List<ProductDto>> GetProductBySalePoint(Guid salePointId, string search, int skipCount, int count)
        {
            return await _productRepository.GetProductsBySalePoint(salePointId, search, skipCount, count);
        }

        public async Task<List<ProductDto>> GetProductBySearch(string search, int skipCount, int count)
        {
            return await _productRepository.GetProductByTitleOrDescription(search, skipCount, count);
        }

        public async Task<ProductDto> GetProductById(Guid productId)
        {
            return await _productRepository.GetProductById(productId);
        }
    }
}
