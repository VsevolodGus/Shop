using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public interface IProductRepository
    {
        public Product GetProductById(Guid Id);

        public List<Product> GetProductsBySearch(string search);

        public List<Product> GetProductsByPrice(decimal lowPrice, decimal highPrice);

        public bool AddProduct(in Product model);

        public bool RemoveProductById(Guid Id);

        public bool IsExistProduct(Guid productId);

        public bool UpdateProduct(in Product model);
    }
}
