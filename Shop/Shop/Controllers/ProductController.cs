using Microsoft.AspNetCore.Mvc;
using Shop.Domain;
using Shop.Domain.DTO;
using Shop.Manager;
using System;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : BaseShopController
    {
        private readonly ProductManager _productService;

        public ProductController(ProductManager productService)
        {
            _productService = productService;
        }


        [HttpPost("set")]
        public async Task<IActionResult> SetProduct([FromBody] ProductEntity model)
        {
            var result = await _productService.SetProductAsync(model);

            if (result != Guid.Empty)
            {
                return JsonCommonApiResult(new
                {
                    errorText = "OK",
                    errorCode = 200,
                    data = result
                });
            }
            else
            {
                return JsonCommonApiResult(new
                {
                    errorText = "Уже существует такоей продукт",
                    errorCode = 700,
                });
            }
        }


        [HttpGet("byid")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _productService.GetProductById(productId);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }


        [HttpGet("search")]
        public async Task<IActionResult> GetProductBySearch([FromQuery] BaseFilter filter)
        {
            var result = await _productService.GetProductBySearch(filter);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }


        [HttpGet("bysalepointid")]
        public async Task<IActionResult> GetProductBySalePoiny(Guid salePointId, [FromQuery] BaseFilter filter)
        {
            var result = await _productService.GetProductBySalePoint(salePointId, filter);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }
    }
}
