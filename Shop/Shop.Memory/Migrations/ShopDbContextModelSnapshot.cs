﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Memory;

namespace Shop.Memory.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    partial class ShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shop.Domain.DTO.ProductDto", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Domain.DTO.ProvidedProductDto", b =>
                {
                    b.Property<long>("PKID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SalePointId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PKID");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalePointId");

                    b.ToTable("ProvidedProducts");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SaleDto", b =>
                {
                    b.Property<long>("PKID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SalePointId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PKID");

                    b.HasIndex("SalePointId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SalePointDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SalePoints");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SalesDataDto", b =>
                {
                    b.Property<long>("PKID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ProductIdAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("ProductQuantity")
                        .HasColumnType("bigint");

                    b.Property<long>("SaleId")
                        .HasColumnType("bigint");

                    b.HasKey("PKID");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleId");

                    b.ToTable("SalesDatas");
                });

            modelBuilder.Entity("Shop.Domain.DTO.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shop.Domain.DTO.ProvidedProductDto", b =>
                {
                    b.HasOne("Shop.Domain.DTO.ProductDto", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Domain.DTO.SalePointDto", "SalePoint")
                        .WithMany("ProvidedProducts")
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SalePoint");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SaleDto", b =>
                {
                    b.HasOne("Shop.Domain.DTO.SalePointDto", "SalePoint")
                        .WithMany()
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Domain.DTO.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("SalePoint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SalesDataDto", b =>
                {
                    b.HasOne("Shop.Domain.DTO.ProductDto", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Domain.DTO.SaleDto", "Sale")
                        .WithMany("SalesDatas")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SaleDto", b =>
                {
                    b.Navigation("SalesDatas");
                });

            modelBuilder.Entity("Shop.Domain.DTO.SalePointDto", b =>
                {
                    b.Navigation("ProvidedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}