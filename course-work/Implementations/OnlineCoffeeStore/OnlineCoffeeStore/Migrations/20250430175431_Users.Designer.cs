﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineCoffeeStore.Data;

#nullable disable

namespace OnlineCoffeeStore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250430175431_Users")]
    partial class Users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineCoffeeStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Espresso"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Latte"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Mocha"
                        });
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.Coffee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Coffees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Ingredients = "Espresso",
                            Name = "Espresso Shot",
                            Price = 2.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Ingredients = "Espresso, Milk, Ice",
                            Name = "Iced Latte",
                            Price = 3.5,
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Ingredients = "Espresso, Milk, Chocolate Syrup, Ice",
                            Name = "Mocha Frappe",
                            Price = 4.0,
                            Status = 1
                        });
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoffeeId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.Coffee", b =>
                {
                    b.HasOne("OnlineCoffeeStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.Order", b =>
                {
                    b.HasOne("OnlineCoffeeStore.Models.User", "Users")
                        .WithMany("Orders")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.OrderItem", b =>
                {
                    b.HasOne("OnlineCoffeeStore.Models.Coffee", "Coffee")
                        .WithMany()
                        .HasForeignKey("CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineCoffeeStore.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coffee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OnlineCoffeeStore.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
