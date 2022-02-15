using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.DTO;
using Shop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    class ShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProductDto> Products{ get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        { }

        //public ShopDbContext()
        //{ }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    //optionsBuilder.UseSqlServer(_connectionString);
        //}
    }
}
