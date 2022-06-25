using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using UserUtils;
using Shop.Domain.InterfaceRepository;
using Shop.Domain.DTO;

namespace Shop.Manager
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> AuthorizationUser(string login, string password)
        {
            var user = await GetUser(login, password);
            if (user is null)
                return null;

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


        public async Task<string> RegistrUser(string login, string password)
        {
            var user = await GetUser(login, password);
            if (user is not null)
                return null;

            await _userRepository.AddAsync(login, password);

            return await AuthorizationUser(login, password);
        }
        private async Task<UserEntity> GetUser(string login, string password)
        {
            var passwordHash = Util.CalculateMD5Hash(password);
            var user = await _userRepository.GetUserForLogin(login, passwordHash);
            return user;
        }

        public async Task<bool> DeleteAcountUser(string auth)
        {
            if (!AuthUtil.IsAuthUser(auth, out Guid userId) && userId != Guid.Empty)
                return false;

            AuthUtil.LogOutUser(auth);
            await _userRepository.DeleteUserById(userId);
            return true;
        }

        public void LogOut(string token)
        {
            AuthUtil.LogOutUser(token);
        }
    }
}
