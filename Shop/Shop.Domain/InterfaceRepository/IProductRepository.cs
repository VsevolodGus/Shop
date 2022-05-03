using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface IProductRepository
    {
        Task<ProductDto> GetProductById(Guid productId);

        Task<List<ProductDto>> GetProductByTitleOrDescription(string search, int skipCount, int count);

        Task<List<ProductDto>> GetProductsBySalePoint(Guid salePointId, string search, int skipCount, int count);

        Task<Guid> AddProduct(ProductDto product);

        Task<Guid> UpdateProduct(ProductDto product);
    }
}
