using System;

namespace InventoryWeb.Dto;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateOnly CreatedAt { get; set; }
    public List<InvoiceItemDto>? Items { get; set; }
}

public class InvoiceItemDto
{
    public int InvoiceItemId { get; set; }
    public int ProductId { get; set; }
    public int InvoiceId { get; set; }
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }

    // Optional: include lightweight product info
    public string? ProductName { get; set; }
    public decimal? ProductPrice { get; set; }
}

public class InvoiceItemsDto
{
    public int InvoiceId { get; set; }
    public string? CustomerName { get; set; }
    public DateOnly Date { get; set; }
    public decimal Total { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal SellingPrice { get; set; }
    // public List<InvoiceItemRequestDto>? Items { get; set; }
}

public class InvoiceRequestDto
{
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateOnly CreatedAt { get; set; }
    public List<InvoiceItemRequestDto>? Items { get; set; }
}

public class InvoiceItemRequestDto
{
    public int ProductId { get; set; }
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
}


public class InvoiceGetDto
{
    public int InvoiceId { get; set; }
    public string? CustomerName { get; set; }
    public DateOnly CreatedAt { get; set; }
    public decimal Total {get;set;}
    public List<InvoiceItemGetDto>? Items { get; set; }
}

public class InvoiceItemGetDto
{
    public string? ProductName { get; set; }
    public decimal Stock { get; set; }
    public decimal Quantity { get; set; }
    public decimal ProductPrice { get; set; }
}