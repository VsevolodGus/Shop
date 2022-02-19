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
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            this._productService = productService;
        }


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
    }
}
