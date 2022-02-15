using System;

namespace Shop.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public User(string name, string password)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Password = password;
        }

        public User()
        {}
    }
}
