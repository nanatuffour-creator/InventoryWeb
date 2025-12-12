using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<GetPurchaseDto> GetPurchase()
    {
        var query = from u in _context.Purchases
                    join s in _context.Supplier on u.Id equals s.Id
                    select new GetPurchaseDto
                    {
                        PurchaseId = u.PurchaseId,
                        SupplierName = s.Name,
                        Amount = u.Amount,
                        Date = u.Date,
                        Stat = u.Stat,
                        PurchaseOrders = (
                            from m in _context.PurchaseOrder
                            join g in _context.Products on m.ProductId equals g.ProductId
                            where m.PurchaseId == u.PurchaseId
                            select new GetPurchaseOrdersDto
                            {
                                ProductName = g.ProductName,
                                CostPrice = m.CostPrice,
                                Quantity = m.Quantity
                            }
                        ).ToList()
                    };

        return [.. query];
    }

    public IEnumerable<GetPurchaseDto> GetPurchasesCompleted()
    {
        var query = from u in _context.Purchases
                    join s in _context.Supplier on u.Id equals s.Id
                    where u.Stat == Enum.Status.Completed
                    select new GetPurchaseDto
                    {
                        PurchaseId = u.PurchaseId,
                        SupplierName = s.Name,
                        Amount = u.Amount,
                        Date = u.Date,
                        Stat = u.Stat,
                        PurchaseOrders = (
                            from m in _context.PurchaseOrder
                            join g in _context.Products on m.ProductId equals g.ProductId
                            where m.PurchaseId == u.PurchaseId
                            select new GetPurchaseOrdersDto
                            {
                                ProductName = g.ProductName,
                                CostPrice = m.CostPrice,
                                Quantity = m.Quantity
                            }
                        ).ToList()
                    };

        return [.. query];
    }

    public IEnumerable<GetPurchaseDto> GetPurchasesPending()
    {
        var query = from u in _context.Purchases
                    join s in _context.Supplier on u.Id equals s.Id
                    where u.Stat == Enum.Status.Pending
                    select new GetPurchaseDto
                    {
                        PurchaseId = u.PurchaseId,
                        SupplierName = s.Name,
                        Amount = u.Amount,
                        Date = u.Date,
                        Stat = u.Stat,
                        PurchaseOrders = (
                            from m in _context.PurchaseOrder
                            join g in _context.Products on m.ProductId equals g.ProductId
                            where m.PurchaseId == u.PurchaseId
                            select new GetPurchaseOrdersDto
                            {
                                ProductName = g.ProductName,
                                CostPrice = m.CostPrice,
                                Quantity = m.Quantity
                            }
                        ).ToList()
                    };

        return [.. query];
    }

    public IEnumerable<GetPurchaseDto> GetPurchasesDelayed()
    {
        var query = from u in _context.Purchases
                    join s in _context.Supplier on u.Id equals s.Id
                    where u.Stat == Enum.Status.Delayed
                    select new GetPurchaseDto
                    {
                        PurchaseId = u.PurchaseId,
                        SupplierName = s.Name,
                        Amount = u.Amount,
                        Date = u.Date,
                        Stat = u.Stat,
                        PurchaseOrders = (
                            from m in _context.PurchaseOrder
                            join g in _context.Products on m.ProductId equals g.ProductId
                            where m.PurchaseId == u.PurchaseId
                            select new GetPurchaseOrdersDto
                            {
                                ProductName = g.ProductName,
                                CostPrice = m.CostPrice,
                                Quantity = m.Quantity
                            }
                        ).ToList()
                    };

        return [.. query];
    }

    public decimal GetTotalPurchase()
    {
        var total = _context.Purchases.Sum(i => i.Amount);
        return total;
    }

    public int GetPendingOrders()
    {
        int pending = _context.Purchases.Count(i => i.Stat == Enum.Status.Pending);
        return pending;
    }

    public int GetCompletedOrders()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int completed = _context.Purchases
                        .Where(e => e.Date == today)
                        .Count(i => i.Stat == Enum.Status.Completed);
        return completed;
    }

}
