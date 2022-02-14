
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//namespace Shop.Login
//{
//    public class Class1
//    {
//        void asd(HttpContext context)
//        {
//            var builder = WebApplication.CreateBuilder();

//            builder.Services.AddAuthorization();
//            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidIssuer = AuthOptions.ISSUER,
//                        ValidateAudience = true,
//                        ValidAudience = AuthOptions.AUDIENCE,
//                        ValidateLifetime = true,
//                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//                        ValidateIssuerSigningKey = true
//                    };
//                });
//            var app = builder.Build();

//            app.UseDefaultFiles();
//            app.UseStaticFiles();

//            app.UseAuthentication();
//            app.UseAuthorization();

//        }
//    }
//}
