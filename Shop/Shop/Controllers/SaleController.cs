using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain;
using Shop.Manager;
using Shop.Manager.Models;
using UserUtils;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/sale")]
    public class SaleController : BaseShopController
    {
        private readonly SaleManager _saleManager;

        public SaleController(SaleManager saleManager)
        {
            this._saleManager = saleManager;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetSales([FromQuery] SalesFilter filter)
        {
            var result = await _saleManager.GetSales(filter);

            return JsonCommonApiResult(new
            {
                errorCode = 200,
                errorText = "OK",
                data = result,
            });
        }

       

        [HttpPost("add/product")]
        public async Task<IActionResult> AddProductInSale([FromBody] OperationProductInSale model, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            await _saleManager.AddProductInSale(model, userId);

            return Content("OK");
        }

        [HttpDelete("remove/product")]
        public async Task<IActionResult> RemoveProductAsync(long saleId, Guid productId, long productCount, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            await _saleManager.RemoveProductFromSale(saleId, productId, productCount, userId);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(Guid salePointId, string comment, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            Guid? currentUserId = userId != Guid.Empty ? userId : null;
            var result = await _saleManager.CreateSale(currentUserId, salePointId, comment);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }

        [HttpPost("cancled")]
        public async Task<IActionResult> CancledAsync(long saleId, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            await _saleManager.CancelSale(saleId, userId);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
            });
        }
    }
}
