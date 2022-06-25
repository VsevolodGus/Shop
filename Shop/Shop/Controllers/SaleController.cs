using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetSales([FromQuery] SalesFilter filter, string auth, int skipCount = 0, int count = 10)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId)
                && filter is null
                && (userId == Guid.Empty && !filter.UserId.HasValue || userId != filter.UserId))
            {
                return Unauthorized();
            }


            var result = await _saleManager.GetSales(filter, skipCount, count);

            return JsonCommonApiResult(new
            {
                errorCode = 200,
                errorText = "OK",
                data = result,
            });
        }

        public class OperationProductInSale
        {
            public long SaleId { get; init; }

            public Guid ProductId { get; init; }

            public long? Count { get; init; }
        }

        [HttpPost("add/product")]
        public async Task<IActionResult> AddProductInSale([FromBody] OperationProductInSale model, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            await _saleManager.AddProductInSale(model.SaleId, model.ProductId, model.Count, userId);

            return Content("OK");
        }

        [HttpDelete("remove/product")]
        public async Task<IActionResult> RemoveProductAsync(long saleId, Guid productId, long? productCount, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            await _saleManager.RemoveProductFromSale(saleId, productId, productCount.GetValueOrDefault(), userId, productCount.HasValue);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(Guid salePointId, string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _saleManager.CreateSale(userId != Guid.Empty ? userId : null, salePointId);

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
