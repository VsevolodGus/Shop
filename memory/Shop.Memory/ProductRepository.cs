using Shop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>()
            {
                // сюда нужно заполнить примерные модели
            };
        }

        public Product GetProductById(Guid Id)
        {
            return _products.FirstOrDefault(c=> c.ProductId == Id);
        }

        public List<Product> GetProductsBySearch(string search)
        {
            return _products.Where(c => c.Name.Contains(search) || c.Description.Contains(search)).ToList();
        }

        public List<Product> GetProductsByPrice(decimal lowPrice, decimal highPrice)
        {
            return _products.Where(c => c.Price >= lowPrice && c.Price<= highPrice).ToList();
        }

        public bool AddProduct(Product model)
        {
            try
            {
                _products.Add(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveProductById(Guid Id)
        {
            var removeProduct = _products.FirstOrDefault(c => c.ProductId == Id);

            if (removeProduct == null)
            {
                return false;
            }
            _products.Remove(removeProduct);
            return true;
        }

        public bool IsExistProduct(Guid productId)
        {
            return _products.Any(c => c.ProductId == productId);
        }

        public bool UpdateProduct(Product model)
        {
            try
            {
                var product = _products.FirstOrDefault(c => c.ProductId == model.ProductId);

                product = new Product(model.ProductId, model.Name, model.Price, model.Description);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
