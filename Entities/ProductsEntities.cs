using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryWeb.Entities;

public class ProductsEntities
{
    [Key]
    public int ProductId { get; set; }   // PK
    public string? ProductName { get; set; }
    public string? ProductImage { get; set; }
    public string? ProductDescription { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    // Foreign Key
    public int CategoryId { get; set; }
    // Navigation Property (Each product belongs to one Category)
    public CategoryEntities? Category { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; } = [];
    public ICollection<PurchaseOrderEntity> PurchaseOrders { get; set; } = [];
}



