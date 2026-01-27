using Microsoft.EntityFrameworkCore;
using SimpleShop.Models;

namespace SimpleShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // Seed some data when the database is created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 999.99m, ImageUrl = "https://placehold.co/300?text=Laptop" },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model", Price = 699.50m, ImageUrl = "https://placehold.co/300?text=Phone" },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling", Price = 199.00m, ImageUrl = "https://placehold.co/300?text=Headphones" }
            );
        }
    }
}