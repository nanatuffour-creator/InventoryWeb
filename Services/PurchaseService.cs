using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;

namespace InventoryWeb.Services;

public class PurchaseService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public void Add(PurchaseDto dto)
    {
        if (dto.Amount <= 0)
        {
            Results.BadRequest("Total Amount must be greater than 0");
            return;
        }
        var purchases = new PurchasesEntity
        {
            PurchaseId = dto.PurchaseId,
            Id = dto.Id,
            Amount = dto.Amount,
            Date = dto.Date,
            Stat = dto.Stat,
            PurchaseOrders = [.. dto.PurchaseOrders!.Select(p => new PurchaseOrderEntity
            {
                PurchaseItemId = p.PurchaseItemId,
                CostPrice = p.CostPrice,
                Quantity = p.Quantity,
                ProductId = p.ProductId,
                PurchaseId = p.PurchaseId
            })]
        };
        _context.Purchases.Add(purchases);
        _context.SaveChanges();
    }

    public IEnumerable<PurchasesEntity> GetPurchase()
    {
        return [.. _context.Purchases];
    }

    // public bool EditPurchase(int id)
    // {
    //     var f = _context.Purchases.FirstOrDefault(p => p.Id == id);

    //     if (f is null) 
    //         return false;


    //     return true;
    // }
}
