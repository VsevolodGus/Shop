using Shop.Memory;
using System;
using System.Collections.Generic;

namespace Shop.App
{
    public class ProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public bool SetProduct(in Product model)
        {
            if (_productRepository.IsExistProduct(model.ProductId))
            {
                return _productRepository.UpdateProduct(model);
            }
            else
            {
                return _productRepository.AddProduct(model);
            }
        }

        public bool RemoveProductById(Guid Id)
        {
            return _productRepository.RemoveProductById(Id);
        }

        public Product GetProductById(Guid productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public List<Product> GetProductsBySearch(string search)
        {
            return _productRepository.GetProductsBySearch(search);
        }

        public List<Product> GetProductsByPrice(decimal lowPrice, decimal highPrice)
        {
            return _productRepository.GetProductsByPrice(lowPrice, highPrice);
        }


    }
}
