using Microsoft.EntityFrameworkCore;
using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Coffee> Coffees => Set<Coffee>(); 
        public DbSet<Category> Categories=> Set<Category>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Espresso" },
                new Category { Id = 2, CategoryName = "Latte" },
                new Category { Id = 3, CategoryName = "Mocha" }
            );

            // Seeding Coffees
            modelBuilder.Entity<Coffee>().HasData(
                new Coffee { Id = 1, Name = "Espresso Shot", Ingredients = "Espresso", Price = 2.00, Status = CoffeeStatus.Available, CategoryId = 1 },
                new Coffee { Id = 2, Name = "Iced Latte", Ingredients = "Espresso, Milk, Ice", Price = 3.50, Status = CoffeeStatus.Available, CategoryId = 2 },
                new Coffee { Id = 3, Name = "Mocha Frappe", Ingredients = "Espresso, Milk, Chocolate Syrup, Ice", Price = 4.00, Status = CoffeeStatus.Pending, CategoryId = 3 }
            );

                modelBuilder.Entity<Order>()
               .HasOne(o => o.Users)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UsersId)
               .OnDelete(DeleteBehavior.Restrict); // За да няма каскадно изтриване

                modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Coffee)
                .WithMany()
                .HasForeignKey(oi => oi.CoffeeId)
                .OnDelete(DeleteBehavior.Restrict);

                 modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Coffee>()
                .Property(c => c.Ingredients)
                .HasMaxLength(255); // or whatever size fits your data

        }


    }
}
