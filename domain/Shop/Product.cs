using System;

namespace Shop
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Product(Guid productId, string name, decimal price, string decription)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Price = price;
            this.Description = decription;
        }

        

    }
}
