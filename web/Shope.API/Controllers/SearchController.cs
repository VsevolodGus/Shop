using Microsoft.AspNetCore.Mvc;
using Shop;
using Shop.App;
using Shope.API.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.API.Controllers
{
    [ApiController]
    [Route("shop/search/")]
    public class SearchController : ShopBaseController
    {
        private ProductService _productService;
        private SalePointService _salePointService;

        public SearchController(ProductService productRepository, SalePointService salePointService)
        {
            this._productService = productRepository;
            this._salePointService = salePointService;
        }

        #region Продукты, услуги, товары
        
        [HttpGet]
        public IActionResult GetProduct(Guid productId)
        {
            var product = _productService.GetProductById(productId);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = product,
            });
        }

        [HttpGet]
        public IActionResult GetProductBySearch(string search)
        {
            var product = _productService.GetProductsBySearch(search);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = product,
            });
        }

        [HttpGet]
        public IActionResult GetProductByRangePrice(decimal lowPrice, decimal highPrice)
        {
            var product = _productService.GetProductsByPrice(lowPrice, highPrice);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = product,
            });
        }
        #endregion

        #region Точки продажи

        [HttpGet]
        public IActionResult GetSalePointById(Guid salePointId)
        {
            var result = _salePointService.GetSalePointId(salePointId);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }


        [HttpGet]
        public IActionResult GetSalePointBySearch(string search)
        {
            var result = _salePointService.GetSalePointBySearch(search);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }
        #endregion


        #region Comment
        //public IActionResult SetProduct([FromBody] Product product)
        //{
        //    var successAdditon = _productService.SetProduct(product);

        //    if (successAdditon)
        //    {
        //        return JsonCommonApiResult(new
        //        {
        //            errorText = "OK",
        //            errorCode = 200,
        //        });
        //    }
        //    else
        //    {
        //        return JsonCommonApiResult(new
        //        {
        //            errorText = "Продукт не добавился/обновился",
        //            errorCode = 601,
        //        });
        //    }
        //}

        //public IActionResult RemoveProduct(Guid productId)
        //{
        //    var successAdditon = _productService.RemoveProductById(productId);

        //    if (successAdditon)
        //    {
        //        return JsonCommonApiResult(new
        //        {
        //            errorText = "OK",
        //            errorCode = 200,
        //        });
        //    }
        //    else
        //    {
        //        return JsonCommonApiResult(new
        //        {
        //            errorText = "Продукт не удалися",
        //            errorCode = 701,
        //        });
        //    }
        //}
        #endregion

    }
}
