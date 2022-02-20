using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseShopController : ControllerBase
    {

        /// <summary>
        /// auth и id юзера пользователя
        /// </summary>
        protected static Dictionary<string, Guid> authUsers = new Dictionary<string, Guid>();

        [NonAction]
        protected static bool ChekAuthToken(string token, out Guid userId)
        {
            if (authUsers.TryGetValue(token, out userId))
            {
                return true;
            }
            else
            {
                userId = Guid.Empty;
                return false;
            }
        }

        [NonAction]
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
