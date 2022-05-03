using Shop.Domain.DTO;
using Shop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shop.DomainTest
{
    public class SaleTest
    {
        const decimal PriceProduct = 10;
        private static Sale CreateTestOrder(Product product, int count = 0)
        {
            if (product is null)
            {
                return new Sale(new SaleEntity
                {
                    PKID = 1,
                    SalesDatas = new List<SalesDataDto>()
                });
            }
            else
            {
                return new Sale(new SaleEntity
                {
                    PKID = 1,
                    SalesDatas = new List<SalesDataDto>()
                    {
                        new SalesDataDto()
                        {
                            ProductId = product.ProductId,
                            ProductQuantity =count,
                            ProductPrice = PriceProduct
                        },
                    }
                });
            }
        }

        private static Product CreateTestProduct()
        {
            return new Product(new ProductEntity
            {
                ProductId = Guid.NewGuid(),
            });
        }

        [Fact]
        public void AddNewItemInSalesDatas()
        {
            var sale = CreateTestOrder(null);

            sale.FillSale(Guid.NewGuid(), 10, 100);

            Assert.Equal(10, sale.TotalCount);
        }

        [Fact]
        public void AddExistItemInSalesDatas()
        {
            var product = CreateTestProduct();

            var sale = CreateTestOrder(product, 10);

            sale.FillSale(product.ProductId, 10, PriceProduct);

            Assert.Equal(20, sale.TotalCount);
        }


        public void RemoveFullProductFromSale()
        {
            var product = CreateTestProduct();

            var sale = CreateTestOrder(product, 10);

            sale.RemoveItem(product.ProductId, 0, true);

            Assert.Null(sale.SaleDatas.FirstOrDefault(c => c.ProductId == product.ProductId));
        }

        public void RemoveSeveralItemProduct_FromSale()
        {
            var product = CreateTestProduct();

            var sale = CreateTestOrder(product, 10);

            sale.RemoveItem(product.ProductId, 1);

            Assert.Equal(9, sale.TotalCount);
        }

    }
}
