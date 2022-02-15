using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.DTO;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Memory.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public UserRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<bool> AddUser(string name, string password)
        {
            using (var dc = dbContextFactory.Create(typeof(UserRepository)))
            {
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
        }

        public async Task<UserDto> GetUserForLogin(string name, string password)
        {
            using (var dc = dbContextFactory.Create(typeof(UserRepository)))
            {
                return await dc.Users.Where(c => c.Name == name && c.Password == password)
                               .FirstOrDefaultAsync();
            }
        }


        

    }
}
