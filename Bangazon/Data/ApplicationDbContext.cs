using System;
using System.Collections.Generic;
using System.Text;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Order> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related order when OrderProducts entry is removed
            modelBuilder.Entity<Order> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Order)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Product> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related product when OrderProducts entry is removed
            modelBuilder.Entity<Product> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Product)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentType> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Admina",
                LastName = "Straytor",
                StreetAddress = "123 Infinity Way",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            ApplicationUser user2 = new ApplicationUser
            {
                FirstName = "Joe",
                LastName = "Snyder",
                StreetAddress = "100 Infinity Way",
                UserName = "joe@gmail.com",
                NormalizedUserName = "JOE@GMAIL.COM",
                Email = "joe@gmail.com",
                NormalizedEmail = "JOE@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794578",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff1"
            };
            user2.PasswordHash = passwordHash.HashPassword(user2, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user2);

            ApplicationUser user3 = new ApplicationUser
            {
                FirstName = "Allie",
                LastName = "Patton",
                StreetAddress = "300 Main Street",
                UserName = "allie@gmail.com",
                NormalizedUserName = "ALLIE@GMAIL.COM",
                Email = "allie@gmail.com",
                NormalizedEmail = "ALLIE@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794579",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff2"
            };
            user3.PasswordHash = passwordHash.HashPassword(user3, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user3);

            ApplicationUser user4 = new ApplicationUser
            {
                FirstName = "Ellie",
                LastName = "Ash",
                StreetAddress = "400 Main Street",
                UserName = "ellie@gmail.com",
                NormalizedUserName = "ELLIE@GMAIL.COM",
                Email = "ellie@gmail.com",
                NormalizedEmail = "ELLIE@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794570",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff3"
            };
            user4.PasswordHash = passwordHash.HashPassword(user4, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user4);

            ApplicationUser user5 = new ApplicationUser
            {
                FirstName = "Eric",
                LastName = "Taylor",
                StreetAddress = "600 Locust Street",
                UserName = "eric@gmail.com",
                NormalizedUserName = "ERIC@GMAIL.COM",
                Email = "eric@gmail.com",
                NormalizedEmail = "ERIC@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794571",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff4"
            };
            user5.PasswordHash = passwordHash.HashPassword(user5, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user5);

            modelBuilder.Entity<PaymentType> ().HasData (
                new PaymentType()
                {
                    PaymentTypeId = 1,
                    UserId = user.Id,
                    Description = "American Express",
                    AccountNumber = "86753095551212"
                },
                new PaymentType()
                {
                    PaymentTypeId = 2,
                    UserId = user.Id,
                    Description = "Discover",
                    AccountNumber = "4102948572991"
                },
                new PaymentType()
                {
                    PaymentTypeId = 3,
                    UserId = user2.Id,
                    Description = "Visa",
                    AccountNumber = "4102948572992"
                },
                new PaymentType()
                {
                    PaymentTypeId = 4,
                    UserId = user3.Id,
                    Description = "Mastercard",
                    AccountNumber = "4102948572993"
                },
                new PaymentType()
                {
                    PaymentTypeId = 5,
                    UserId = user4.Id,
                    Description = "Discover",
                    AccountNumber = "4102948572994"
                }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType()
                {
                    ProductTypeId = 1,
                    Label = "Sporting Goods"
                },
                new ProductType()
                {
                    ProductTypeId = 2,
                    Label = "Appliances"
                },
                new ProductType()
                {
                    ProductTypeId = 3,
                    Label = "Tools"
                },
                new ProductType()
                {
                    ProductTypeId = 4,
                    Label = "Games"
                },
                new ProductType()
                {
                    ProductTypeId = 5,
                    Label = "Music"
                },
                new ProductType()
                {
                    ProductTypeId = 6,
                    Label = "Health"
                },
                new ProductType()
                {
                    ProductTypeId = 7,
                    Label = "Outdoors"
                },
                new ProductType()
                {
                    ProductTypeId = 8,
                    Label = "Beauty"
                },
                new ProductType()
                {
                    ProductTypeId = 9,
                    Label = "Shoes"
                },
                new ProductType()
                {
                    ProductTypeId = 10,
                    Label = "Automotive"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    UserId = user2.Id,
                    Description = "It flies high",
                    Title = "Kite",
                    Quantity = 100,
                    Price = 2.99
                },
                new Product()
                {
                    ProductId = 2,
                    ProductTypeId = 2,
                    UserId = user2.Id,
                    Description = "It rolls fast",
                    Title = "Wheelbarrow",
                    Quantity = 5,
                    Price = 29.99
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTypeId = 3,
                    UserId = user3.Id,
                    Description = "It cuts things",
                    Title = "Saw",
                    Quantity = 18,
                    Price = 31.49
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTypeId = 6,
                    UserId = user3.Id,
                    Description = "It stops scurvy",
                    Title = "Toothpaste",
                    Quantity = 50,
                    Price = 7.99
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 2,
                    UserId = user3.Id,
                    Description = "It rolls!",
                    Title = "Cinnamon Rolls",
                    Quantity = 1000,
                    Price = 99.99
                },
                new Product()
                {
                    ProductId = 6,
                    ProductTypeId = 10,
                    UserId = user4.Id,
                    Description = "It is indestructible (almost)",
                    Title = "Cyber Truck",
                    Quantity = 18,
                    Price = 9999.99
                },
                new Product()
                {
                    ProductId = 7,
                    ProductTypeId = 9,
                    UserId = user4.Id,
                    Description = "Go like never before!",
                    Title = "Skechers",
                    Quantity = 30,
                    Price = 24.89
                },
                new Product()
                {
                    ProductId = 8,
                    ProductTypeId = 4,
                    UserId = user5.Id,
                    Description = "Don't try this at home.",
                    Title = "Twister",
                    Quantity = 32,
                    Price = 22.69
                },
                new Product()
                {
                    ProductId = 9,
                    ProductTypeId = 5,
                    UserId = user5.Id,
                    Description = "It gets banged on (with drumsticks)",
                    Title = "Drumset",
                    Quantity = 10,
                    Price = 475.99
                }
            );

            modelBuilder.Entity<Order> ().HasData (
                new Order()
                {
                    OrderId = 1,
                    UserId = user2.Id,
                    PaymentTypeId = 3
                },
                new Order()
                {
                    OrderId = 2,
                    UserId = user3.Id,
                    PaymentTypeId = 4
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct()
                {
                    OrderProductId = 1,
                    OrderId = 1,
                    ProductId = 9
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct()
                {
                    OrderProductId = 2,
                    OrderId = 2,
                    ProductId = 7
                }
            );
        }

        public DbSet<Bangazon.Models.Account> Account { get; set; }
    }
}