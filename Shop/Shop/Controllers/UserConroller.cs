using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain;
using Shop.Domain.InterfaceRepository;
using Swashbuckle.AspNetCore.Annotations;
using Shop.Manager;

namespace Shop.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserConroller : BaseShopController
    {
        private readonly UserManager _userService;
        public UserConroller(UserManager userService)
        {
            this._userService = userService;
        }

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var tokenString = await _userService.AuthorizationUser(model.UserName, model.Password);

            if (tokenString is not null)
            {
                return JsonCommonApiResult(new
                {
                    errorText = "OK",
                    errorCode = 200,
                    data = tokenString
                });
            }
            else
            {
                return Unauthorized();
            }
        }



        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("logout")]
        public async Task<IActionResult> LogOut(string token)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _userService.LogOut(token);
            return JsonCommonApiResult(new
            {
                errorText = "OK",
                errorCode = 200,
            });
        }


        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("registr")]
        public async Task<IActionResult> Registr(LoginModel model)
        {
            var token = await _userService.RegistrUser(model.UserName, model.Password);

            if (token is not null)
            {
                return JsonCommonApiResult(new
                {
                    errorText = "OK",
                    errorCode = 200,
                    data = token
                });
            }
            else
            {
                return JsonCommonApiResult(new
                {
                    errorText = "NO OK",
                    errorCode = 401,
                });
            }
        }


        [HttpPost, Route("delete")]
        public async Task<IActionResult> DeleteAccountUser(string auth)
        {
            var isDelete = await _userService.DeleteAcountUser(auth);

            if (isDelete)
            {
                return JsonCommonApiResult(new
                {
                    errorText = "OK",
                    errorCode = 200,
                });
            }
            else
            {
                return JsonCommonApiResult(new
                {
                    errorText = "NO OK",
                    errorCode = 401,
                });
            }

        }
    }
}

