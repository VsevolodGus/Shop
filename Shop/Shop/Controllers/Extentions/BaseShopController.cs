using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shop.Controllers
{
    [ApiController]
    public class BaseShopController : ControllerBase
    {
        [NonAction]
        // расширить метод, чтобы передавать код и текст ошибки
        protected IActionResult JsonCommonApiResult(object model)
        {
            return Content(JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter(), new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'" } },
                ContractResolver = null
            }), "application/json");
        }
    }
}
