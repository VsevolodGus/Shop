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
    [Route("api/sale")]
    [ApiController]
    public class SaleController : BaseShopController
    {
        private readonly SaleManager _saleManager;

        public SaleController(SaleManager saleManager)
        {
            this._saleManager = saleManager;
        }
        

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetSales([FromQuery] SalesFilter filter, int skipCount = 0, int count = 10)
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
