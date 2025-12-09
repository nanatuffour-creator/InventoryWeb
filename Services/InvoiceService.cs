using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Services;

public class InvoiceService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;


    public void AddInvoice(InvoiceDto dto)
    {
        if (dto.TotalAmount <= 0)
        {
            Results.BadRequest("Total Amount must be greater than 0");
            return; // make sure to stop execution
        }

        var invoice = new Invoice
        {
            InvoiceId = dto.InvoiceId,
            CustomerId = dto.CustomerId,
            TotalAmount = dto.TotalAmount,
            CreatedAt = dto.CreatedAt,
            InvoiceItems = dto.Items?
                .Select(i => new InvoiceItem
                {
                    InvoiceItemId = i.InvoiceItemId,
                    ProductId = i.ProductId,
                    InvoiceId = dto.InvoiceId,
                    SellingPrice = i.SellingPrice,
                    Quantity = i.Quantity
                })
                .ToList()
        };

        _context.Invoices.Add(invoice);
        _context.SaveChanges();
    }

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

}
