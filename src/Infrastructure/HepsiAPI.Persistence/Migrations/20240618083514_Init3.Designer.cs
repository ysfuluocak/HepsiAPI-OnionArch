﻿// <auto-generated />
using System;
using HepsiAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HepsiAPI.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240618083514_Init3")]
    partial class Init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandName = "Asus",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 703, DateTimeKind.Local).AddTicks(9074),
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            BrandName = "Hp",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 703, DateTimeKind.Local).AddTicks(9087),
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            BrandName = "Mango",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 703, DateTimeKind.Local).AddTicks(9088),
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Elektronik",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 704, DateTimeKind.Local).AddTicks(308),
                            IsDeleted = false,
                            ParentId = 0,
                            Priority = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Moda",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 704, DateTimeKind.Local).AddTicks(310),
                            IsDeleted = false,
                            ParentId = 0,
                            Priority = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Bilgisayar",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 704, DateTimeKind.Local).AddTicks(311),
                            IsDeleted = false,
                            ParentId = 1,
                            Priority = 1
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Kadin",
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 704, DateTimeKind.Local).AddTicks(312),
                            IsDeleted = false,
                            ParentId = 2,
                            Priority = 1
                        });
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.CategoryProduct", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CategoryProducts");
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 704, DateTimeKind.Local).AddTicks(5223),
                            Description = "Oyun Bilgisayari",
                            Discount = 0.5m,
                            IsDeleted = false,
                            Price = 45000m,
                            Title = "Bilgisayar"
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 3,
                            CreatedDate = new DateTime(2024, 6, 18, 11, 35, 14, 704, DateTimeKind.Local).AddTicks(5231),
                            Description = "Abiye",
                            Discount = 0.3m,
                            IsDeleted = false,
                            Price = 50000m,
                            Title = "Elbise"
                        });
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.CategoryProduct", b =>
                {
                    b.HasOne("HepsiAPI.Domain.Entities.Category", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HepsiAPI.Domain.Entities.Product", "Product")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Product", b =>
                {
                    b.HasOne("HepsiAPI.Domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Category", b =>
                {
                    b.Navigation("CategoryProducts");
                });

            modelBuilder.Entity("HepsiAPI.Domain.Entities.Product", b =>
                {
                    b.Navigation("CategoryProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
