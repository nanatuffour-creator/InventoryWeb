using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryWeb.Entities;

public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public Customer? Customer { get; set; }
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public List<InvoiceItem> InvoiceItems { get; set; } = new();
    public string? UserId { get; set; } //Foreign key to UserEntities
    public UserEntities? User { get; set; } //Navigation property to UserEntities
}

