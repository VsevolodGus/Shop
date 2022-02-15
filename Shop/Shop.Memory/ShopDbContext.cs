using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;

namespace Shop.Memory
{
    public class ShopDbContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<ProductDto> Products{ get; set; }
        public DbSet<ProvidedProductDto> ProvidedProducts { get; set; }
        public DbSet<SalePointDto> SalePoints { get; set; }
        public DbSet<SaleDto> Sales { get; set; }
        public DbSet<SalesDataDto> SalesDatas { get; set; }


        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    //optionsBuilder.UseSqlServer(_connectionString);
        //}
    }
}
