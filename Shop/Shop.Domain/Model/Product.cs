using Shop.Domain.DTO;
using System;

namespace Shop.Domain.Model
{
    public class Product
    {
        private readonly ProductEntity _productDto;

        public Product(ProductEntity productDto)
        {
            this._productDto = productDto;
        }
        public Guid ProductId
        {
            get => _productDto.ProductId;
        }

        public string Name
        {
            get => _productDto.Name;
            set => _productDto.Name = value;
        }

        public decimal Price
        {
            get => _productDto.Price;
            set => _productDto.Price = value;
        }


        public static class Mapper
        {
            public static Product Map(ProductEntity dto) => new Product(dto);

            public static ProductEntity Map(Product domain) => domain._productDto;
        }
    }
}
