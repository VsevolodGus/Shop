using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manager
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<bool> SetNewProduct(ProductDto product)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> UpdateProduct(ProductDto product)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> SetProduct(ProductDto product)
        {
            var productExists = await _productRepository.GetProductById(product.ProductId);
            try
            {
                if (productExists is null)
                {
                    return await _productRepository.AddProduct(product);
                }
                else
                {
                    return await _productRepository.UpdateProduct(product);
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
