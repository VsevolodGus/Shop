using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using UserUtils;
using Shop.Domain.InterfaceRepository;

namespace Shop.Manager
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<string> AuthorizationUser(string login, string password, bool isFirstLogon = false)
        {
            var user = await _userRepository.GetUserForLogin(login, password);

            if (user is not null || isFirstLogon)
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
                AuthUtil.AddUserAuth(tokenString, user.Id);
                return tokenString;
            }


            return null;
        }


        public async Task LogOut(string token)
        {
            AuthUtil.LogOutUser(token);
        }

        public async Task<string> RegistrUser(string login, string password)
        {
            password = Util.CalculateSHA256Hash(password);
            var user = await _userRepository.GetUserForLogin(login, password);
            if (user is null)
            {
                if (await _userRepository.AddUser(login, password) == false)
                    return null;

                return await AuthorizationUser(login, password);
            }

            return null;
        }


        public async Task<bool> DeleteAcountUser(string auth)
        {
            if (AuthUtil.IsAuthUser(auth, out Guid userId) && userId != Guid.Empty)
            {
                AuthUtil.LogOutUser(auth);
                await _userRepository.DeleteUserById(userId);
                return true;
            }

            return false;
        }
        // if (!ChekAuthToken(auth, out Guid currentUserId) && currentUserId != userId)
        //        return Unauthorized();

        //await _userRepository.DeleteUserById(userId);

        //    return JsonCommonApiResult("OK");


    }
}
