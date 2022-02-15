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

    [ApiController]
    [Route("api/search/")]
    public class SearchController : BaseShopController
    {
        IProductRepository _productRepository;
        public SearchController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("/list/product")]
        public async Task<IActionResult> GetListProducts(string search ="", int skipCount = 0, int count =10, string auth = "")
        {
            var result = await _productRepository.GetProductByTitleOrDescription(search, skipCount, count);

            return JsonCommonApiResult(result);
        }

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("/item/product")]
        public async Task<IActionResult> GetProductById(Guid prodcutId)
        {
            var result = await _productRepository.GetProductById(prodcutId);

            return JsonCommonApiResult(result);
        }

    }
}
