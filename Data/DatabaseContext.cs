using System;
using InventoryWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<UserEntities> User { get; set; }
    public DbSet<ProductsEntities> Products { get; set; }
    public DbSet<CategoryEntities> Category { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<PurchasesEntity> Purchases { get; set; }
    public DbSet<PurchaseOrderEntity> PurchaseOrder { get; set; }

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

        modelBuilder.Entity<Invoice>()
            .HasOne(p => p.User)
            .WithMany(c => c.Invoices)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PurchasesEntity>()
            .HasOne(p => p.Suppliers)
            .WithMany(c => c.Purchases)
            .HasForeignKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PurchaseOrderEntity>()
            .HasOne(p => p.Purchases)
            .WithMany(c => c.PurchaseOrders)
            .HasForeignKey(p => p.PurchaseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PurchaseOrderEntity>()
            .HasOne(p => p.Products)
            .WithMany(c => c.PurchaseOrders)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductsEntities>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<PurchasesEntity>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<PurchaseOrderEntity>()
            .Property(p => p.CostPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .ToTable("Invoices")
            .Property(p => p.TotalAmount)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Invoice>().ToTable("Invoices");
        modelBuilder.Entity<InvoiceItem>().ToTable("InvoiceItems");

        modelBuilder.Entity<Invoice>()
            .Property(p => p.CreatedAt)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
            );

        modelBuilder.Entity<InvoiceItem>()
            .ToTable("InvoiceItems")
            .Property(p => p.SellingPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Invoices)
            .HasForeignKey(p => p.CustomerId);

        modelBuilder.Entity<InvoiceItem>()
            .HasOne(ii => ii.Invoice)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(ii => ii.InvoiceId);

        modelBuilder.Entity<InvoiceItem>()
            .HasOne(ii => ii.Product)
            .WithMany(p => p.InvoiceItems)
            .HasForeignKey(ii => ii.ProductId);
    }
}


