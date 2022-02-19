using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Manager;
using Shop.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : BaseShopController
    {
        private readonly SaleManager _saleManager;

        public SaleController(SaleManager saleManager)
        {
            this._saleManager = saleManager;
        }

        //public async Task<IActionResult> SetProductInCart(Guid productId)
        //{
        //    _saleManager.GetSaleAsync()
        //    return null;
        //}


        public async Task<IActionResult> GetSales([FromQuery] SalesFilter filter, int skipCount =0, int count =10)
        {
            var result = await _saleManager.GetSales(filter, skipCount, count);
            return JsonCommonApiResult(new
            {
                errorCode = 200,
                errorText = "OK",
                data = result,
            });
        }
    }
}
