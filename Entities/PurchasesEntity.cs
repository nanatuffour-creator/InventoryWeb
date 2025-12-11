using System;
using System.ComponentModel.DataAnnotations;
using InventoryWeb.Enum;

namespace InventoryWeb.Entities;

public class PurchasesEntity
{
    [Key]
    public int PurchaseId { get; set; }
    public int Id { get; set; }  //Foreign Key
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public Status Stat { get; set; }
    public Supplier? Suppliers { get; set; } //Navigation Property
    public List<PurchaseOrderEntity>? PurchaseOrders { get; set; } = new List<PurchaseOrderEntity>();
}

public class PurchaseOrderEntity
{
    [Key]
    public int PurchaseItemId { get; set; }
    public decimal CostPrice { get; set; }
    public int Quantity { get; set; }
    public int PurchaseId { get; set; } //Foreign Key
    public int ProductId {get;set;} //Foreign Key
    public PurchasesEntity? Purchases { get; set; }
    public ProductsEntities? Products { get; set; }
}
