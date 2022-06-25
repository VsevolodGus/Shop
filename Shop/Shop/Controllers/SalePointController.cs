using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.DTO;
using Shop.Manager;
using Shop.Manager.Models;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/sale/point/")]
    public class SalePointController : BaseShopController
    {
        private readonly SalePointManager _salePointManager;

        public SalePointController(SalePointManager salePointManager)
        {
            _salePointManager = salePointManager;
        }



        [HttpPost("set")]
        public async Task<IActionResult> SetSalePoint([FromBody] SalePointEntity model)
        {
            var result = await _salePointManager.SetSalePoint(model);

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



        [HttpPost("set/product")]
        public async Task<IActionResult> SetProductInAssortiment([FromBody] SettingsAddingProducts model)
        {
            await _salePointManager.AddProductInAssortment(model);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200
            });
        }



        [HttpDelete("delete/empty")]
        public async Task<IActionResult> DeleteEmptProductCalls(Guid? salePointId)
        {
            await _salePointManager.DeleteAbsenceProductFromSalePoint(salePointId);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200
            });
        }



        [HttpGet("byId")]
        public async Task<IActionResult> GetSalePointById(Guid salePointId)
        {
            var result = await _salePointManager.GetSalePointById(salePointId);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }


        // объединить в модельку BaseFilter(search, skipCount, count)
        [HttpGet("list/search")]
        public async Task<IActionResult> GetSalePointBySearch(string search, int skipCount = 0, int count = 10)
        {
            var result = await _salePointManager.GetSalePointBySearch(search, skipCount, count);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }


        // объединить в модельку BaseFilter(search, skipCount, count)
        [HttpGet("list/has/product")]
        public async Task<IActionResult> GetSalePointByProduct(Guid productId, int skipCount = 0, int count = 10)
        {
            var result = await _salePointManager.GetListSalePointWhereHasProductById(productId, skipCount, count);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
                data = result
            });
        }
    }
}
