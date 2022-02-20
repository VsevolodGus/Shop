using Microsoft.AspNetCore.Mvc;
using Shop.Manager;
using Shop.Manager.Models;
using System;
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
        public async Task<IActionResult> GetSales([FromQuery] SalesFilter filter, string auth, int skipCount = 0, int count = 10)
        {
            ChekAuthToken(auth, out Guid userId);
            if ((userId == Guid.Empty && !filter.UserId.HasValue) || userId == filter.UserId)
                return Unauthorized();


            var result = await _saleManager.GetSales(filter, skipCount, count);

            return JsonCommonApiResult(new
            {
                errorCode = 200,
                errorText = "OK",
                data = result,
            });
        }


        [HttpPost]
        [Route("add/product")]
        public async Task<IActionResult> AddProductInSale(long saleId, Guid productId, long productCount, string auth)
        {
            ChekAuthToken(auth, out Guid userId);

            await _saleManager.AddProductInSale(saleId, productId, productCount, userId);

            return Content("OK");
        }

        [HttpDelete]
        [Route("remove/product")]
        public async Task<IActionResult> RemoveProduct(long saleId, Guid productId, long? productCount, string auth)
        {
            ChekAuthToken(auth, out Guid userId);

            await _saleManager.RemoveProductFromSale(saleId, productId, productCount.GetValueOrDefault(), userId, productCount.HasValue);

            return Content("OK");
        }



        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSale(Guid salePointId, string auth)
        {
            ChekAuthToken(auth, out Guid userId);

            await _saleManager.CreateSale(userId != Guid.Empty ? userId : null, salePointId);

            return Content("OK");
        }

        [HttpPost]
        [Route("cancled")]
        public async Task<IActionResult> SetCancledSale(long saleId, string auth)
        {
            ChekAuthToken(auth, out Guid userId);

            await _saleManager.CancelSale(saleId, userId);

            return Content("OK");
        }
    }
}
