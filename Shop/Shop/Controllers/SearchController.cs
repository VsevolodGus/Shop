using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [Route("api/search/")]
    [ApiController]
    public class SearchController : BaseShopController
    {
        IProductRepository _productRepository;
        public SearchController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<IActionResult> GetListProducts(string search ="", int skipCount = 0, int count =10, string auth = "")
        {
            var result = await _productRepository.GetProductByTitleOrDescription(search, skipCount, count);

            return JsonCommonApiResult(result);
        }

        public async Task<IActionResult> GetProductById(Guid prodcutId)
        {
            var result = await _productRepository.GetProductById(prodcutId);

            return JsonCommonApiResult(result);
        }

    }
}
