using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.DTO;
using Shop.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseShopController
    {
        private readonly ProductManager _productService;

        public ProductController(ProductManager productService)
        {
            this._productService = productService;
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SetProduct([FromBody] ProductDto model)
        {
            var result = await _productService.SetProduct(model);

            if (result)
            {
                return JsonCommonApiResult(new
                {
                    errorText = "OK",
                    errorCode = 200
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

        [HttpGet]
        [Route("")]
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProductBySearch(string search, int skipCount = 0, int count = 10)
        {
            var result = await _productService.GetProductBySearch(search, skipCount, count);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProductBySalePoiny(Guid salePointId, string search, int skipCount = 0, int count = 10)
        {
            var result = await _productService.GetProductBySalePoint(salePointId, search, skipCount, count);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }
    }
}
