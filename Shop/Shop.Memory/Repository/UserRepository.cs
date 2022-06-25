using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;

namespace Shop.Memory.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public UserRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<bool> AddAsync(string name, string password)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));

            await dc.Users.AddAsync(new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Password = password,
            });
            await dc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserById(Guid userId)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));


                var user = await dc.Users.Where(c => c.Id == userId && c.IsDeleted == false).FirstOrDefaultAsync();
            if (user is not null)
            {
                user.IsDeleted = false;
                await dc.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserEntity> GetUserForLogin(string name, string password)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));
            return await dc.Users.Where(c => c.Name == name && c.Password == password && c.IsDeleted == false)
                                 .FirstOrDefaultAsync();
        }




    }
}
