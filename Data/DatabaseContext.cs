using System;
using InventoryWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<UserEntities> User { get; set; }
    public DbSet<ProductsEntities> Products { get; set; }
    public DbSet<CategoryEntities> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntities>()
            .Property(u => u.UserId)
            .ValueGeneratedNever();

        modelBuilder.Entity<ProductsEntities>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductsEntities>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}


