using Microsoft.AspNetCore.Mvc;
using Shop.Domain.DTO;
using Shop.Manager;
using Shop.Manager.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/salePoint/")]
    [ApiController]
    public class SalePointController : BaseShopController
    {
        SalePointManager _salePointManager;

        public SalePointController(SalePointManager salePointManager)
        {
            this._salePointManager = salePointManager;
        }

        [HttpPost, Route("set")]
        public async Task<IActionResult> SetSalePoint([FromBody] SalePointDto model)
        {
            var result = await _salePointManager.SetSalePoint(model);


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

        [HttpPost, Route("set")]
        public async Task<IActionResult> SetProductInAssortiment([FromBody] SettingsAddingProducts model)
        {
            await _salePointManager.AddProductInAssortment(model);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200
            });
        }


        [HttpDelete, Route("delete/empty")]
        public async Task<IActionResult> DeleteEmptProductCalls(Guid? salePointId)
        {
            await _salePointManager.DeleteAbsenceProductFromSalePoint(salePointId);

            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200
            });
        }



        [HttpGet, Route("byId")]
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

        [HttpGet, Route("list/search")]
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

        [HttpGet, Route("list/has/product")]
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
