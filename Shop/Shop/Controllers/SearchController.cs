using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : BaseShopController
    {
        
    }
}
