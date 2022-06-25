using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetByIdAsync(Guid productId);

        Task<List<ProductEntity>> GetProductByTitleOrDescription(string search, int skipCount, int count);

        Task<List<ProductEntity>> GetProductsBySalePoint(Guid salePointId, string search, int skipCount, int count);

        Task<Guid> AddProduct(ProductEntity product);

        Task<Guid> UpdateProduct(ProductEntity product);
    }
}
