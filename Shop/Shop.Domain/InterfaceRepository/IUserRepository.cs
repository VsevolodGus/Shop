using Shop.Domain.DTO;
using System;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface IUserRepository
    {
        Task<bool> AddUser(string name, string password);

        Task<UserDto> GetUserForLogin(string name, string password);

        Task<bool> DeleteUserById(Guid userId);
    }
}
