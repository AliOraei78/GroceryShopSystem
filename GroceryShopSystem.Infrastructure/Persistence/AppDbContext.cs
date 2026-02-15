using GroceryShopSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using GroceryShopSystem.Infrastructure.Persistence.Configurations;
using GroceryShopSystem.Core.Security;

namespace GroceryShopSystem.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("product");
        modelBuilder.Entity<Product>().ToTable("user");
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}