using System;

namespace InventoryWeb.Entities;

public class Invoices
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
}
