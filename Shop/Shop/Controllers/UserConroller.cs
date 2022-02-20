using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain;
using Shop.Domain.InterfaceRepository;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConroller : BaseShopController
    {
        private readonly IUserRepository _userRepository;

        public UserConroller(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userRepository.GetUserForLogin(model.UserName, model.Password);

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
            var user = await _userRepository.GetUserForLogin(model.UserName, model.Password);
            if (user is null)
            {
                await _userRepository.AddUser(model.UserName, model.Password);
                return await Login(model);
            }
            else
            {
                return Content("NO OK");
            }
        }


        [HttpPost, Route("delete")]
        public async Task<IActionResult> DeleteAccountUser(Guid userId, string auth)
        {
            if (!ChekAuthToken(auth, out Guid currentUserId) && currentUserId != userId)
                return Unauthorized();

            await _userRepository.DeleteUserById(userId);

            return null;
        }
    }
}

