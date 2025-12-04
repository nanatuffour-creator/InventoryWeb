using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryWeb.Entities;

public class InvoiceItem
{
    [Key]
    public int InvoiceItemId { get; set; }
    public int ProductId { get; set; }
    public int InvoiceId { get; set; }
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }
    // Navigation properties
    public Invoice? Invoice { get; set; }
    public ProductsEntities? Product { get; set; }
}
