using Shop.Domain;
using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Memory.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();

        public bool AddUser(string name, string password)
        {
            try
            {
                users.Add(new User(name, password));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
