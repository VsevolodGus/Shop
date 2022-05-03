using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserUtils;

namespace Shop.Memory.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public UserRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<bool> AddUser(string name, string password)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));

            try
            {
                
                await dc.Users.AddAsync(new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Password = password,
                });
                await dc.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task<UserDto> GetUserForLogin(string name, string password)
        {
            var dc = dbContextFactory.Create(typeof(UserRepository));
            var passwordHash = Util.CalculateSHA256Hash(password);
            return await dc.Users.Where(c => c.Name == name && c.Password == password && c.IsDeleted == false)
                                 .FirstOrDefaultAsync();
        }




    }
}
