using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


    }
}
