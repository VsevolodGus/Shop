using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.API.Extentions
{
    public abstract class ShopBaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult JsonCommonApiResult(object model)
        {
            return Content(JsonConvert.SerializeObject(model));
        }
    }
}
