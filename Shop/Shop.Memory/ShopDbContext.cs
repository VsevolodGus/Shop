using Microsoft.EntityFrameworkCore;
using Shop.Domain.DTO;
using System.Collections.Generic;

namespace Shop.Memory
{
    public class ShopDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<SalePointEntity> SalePoints { get; set; }
        public DbSet<ProvidedProductDto> ProvidedProducts { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<SalesDataDto> SalesDatas { get; set; }


        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        { }


        #region Заполнение базы фейковыми данными
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildModelProducts(modelBuilder);
            BuildModelUsers(modelBuilder);
            BuildModelSalePoints(modelBuilder);
        }

        private static void BuildModelProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>(action =>
            {
                action.HasData(
                    new ProductEntity
                    {
                        ProductId = System.Guid.NewGuid(),
                        Name = "Яйца",
                        IsDeleted = false,
                        Price = 10,
                    },
                    new ProductEntity
                    {
                        ProductId = System.Guid.NewGuid(),
                        Name = "Водка",
                        IsDeleted = false,
                        Price = 100,
                    });
            });
        }

        private static void BuildModelUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(action =>
            {
                action.HasData(
                    new UserEntity
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Seva",
                        IsDeleted = false,
                        Email = "gusakseva8@gmail.com",
                        NumberPhone = "89173620902",
                        Password ="asd"
                    },
                    new UserEntity
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Seva",
                        IsDeleted = false,
                        Email = "vg@gmail.com",
                        NumberPhone = "89183620902",
                        Password = "asd"
                    });
            });
        }

        private static void BuildModelSalePoints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalePointEntity>(action =>
            {
                action.HasData(
                    new SalePointEntity
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Другой магазин",
                        Address = "dsassaas",
                        ProvidedProducts = new List<ProvidedProductDto>(),
                        
                    },
                    new SalePointEntity
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Магазин",
                        Address = "dsassaas",
                        ProvidedProducts = new List<ProvidedProductDto>(),
                    });
            });
        }
        #endregion
    }
}
