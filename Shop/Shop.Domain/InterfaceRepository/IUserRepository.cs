using Shop.Domain.DTO;
using System;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(string name, string password);

        Task<UserEntity> GetUserForLogin(string name, string password);

        Task<bool> DeleteUserById(Guid userId);
    }
}
