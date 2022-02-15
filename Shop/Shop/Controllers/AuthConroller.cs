using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Shop.Domain;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthConroller : BaseShopController
    {
        private List<User> users = new List<User>
        {
            new User()
            {
                Id = Guid.NewGuid(),
                Name = "asd",
                Password = "asd",
            }
        };

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("login")]
        public IActionResult Login(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = users.FirstOrDefault(c => c.Name == model.UserName && c.Password == model.Password);

            if (user is not null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@2410"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "CodeMaze",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                authUsers.Add(tokenString, user.Id);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }



        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Content("OK");
        }


        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("registr")]
        public async Task<IActionResult> Registr(LoginModel model)
        {
            // добавление в базу

            return this.Login(model);
        }
    }
}

