using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Services;

public class InvoiceService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public IEnumerable<InvoiceGetDto> GetInvoices()
    {
        var result = _context.Invoices
            .Include(i => i.Customer)
            .Include(i => i.InvoiceItems)
                .ThenInclude(ii => ii.Product)
            .Select(u => new InvoiceGetDto
            {
                InvoiceId = u.InvoiceId,
                CustomerName = u.Customer!.Name,
                Total = u.TotalAmount,
                CreatedAt = u.CreatedAt,

                Items = u.InvoiceItems!.Select(item => new InvoiceItemGetDto
                {
                    ProductName = item.Product!.ProductName,
                    Stock = item.Quantity,
                    Quantity = item.Quantity,
                    ProductPrice = item.SellingPrice

                }).ToList()
            }).ToList();

        return result;
    }

    public Invoice CreateInvoice(InvoiceRequestDto request)
    {
        var invoice = new Invoice
        {
            CustomerId = request.CustomerId,
            TotalAmount = request.TotalAmount,
            CreatedAt = request.CreatedAt,
            InvoiceItems = [.. request.Items!.Select(i => new InvoiceItem
            {
                ProductId = i.ProductId,
                SellingPrice = i.SellingPrice,
                Quantity = i.Quantity,
            })]
        };

        _context.Invoices.Add(invoice);
        _context.SaveChanges();

        return invoice;
    }

    public decimal GetInvoicesTotal()
    {
        return _context.Invoices.Sum(i => i.TotalAmount);
    }

    public decimal GetInvoicesTotalByDate()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
        DateOnly last30Days = today.AddDays(-30);

        var totalLast30Days = _context.Invoices
            .Where(i => i.CreatedAt >= last30Days && i.CreatedAt <= today)
            .Sum(i => i.TotalAmount);

        var totalAllInvoices = _context.Invoices.Sum(i => i.TotalAmount);

        if (totalAllInvoices == 0) return 0;

        var percentage = (totalLast30Days / totalAllInvoices) * 100;
        return Math.Round(percentage, 0);
    }

    public decimal GetInvoicesTotalForToday()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        // Count invoices created today
        int countToday = _context.Invoices
            .Count(i => i.CreatedAt == today);

        return countToday;
    }

}
