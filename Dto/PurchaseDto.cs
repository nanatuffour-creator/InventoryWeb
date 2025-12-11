using System;
using InventoryWeb.Enum;

namespace InventoryWeb.Dto;

public class PurchaseDto
{
    public int PurchaseId { get; set; }
    public int Id { get; set; }  //Foreign Key
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public Status Stat { get; set; }
    public List<PurchaseOrderDto>? PurchaseOrders { get; set; }
}

public class PurchaseOrderDto
{
    public int PurchaseItemId { get; set; }
    public decimal CostPrice { get; set; }
    public int Quantity { get; set; }
    public int PurchaseId { get; set; } 
    public int ProductId { get; set; } 
}
