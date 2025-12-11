using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace InventoryWeb.Services;

public class SupplierService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public void AddSupplier(SuppliersDto dto)
    {
        var newSupplier = new Supplier
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            CreatedAt = dto.CreatedAt
        };
        _context.Supplier.Add(newSupplier);
        _context.SaveChanges();
    }

    public IEnumerable<Supplier> GetSuppliers()
    {
        return [.. _context.Supplier];
    }

    public bool Deletesupplier(string supplierName)
    {
        var supplier = _context.Supplier.FirstOrDefault(p => p.Name == supplierName);
        if (supplier is null) return false;

        _context.Supplier.Remove(supplier);
        _context.SaveChanges();
        return true;
    }

    public bool EditSupplier(SupplierDto dto)
    {
        var newName = _context.Supplier.FirstOrDefault(s => s.Id == dto.Id);
        if (newName is null) return false;

        newName.Name = dto.Name;
        newName.Email = dto.Email;
        newName.Phone = dto.Phone;

        _context.SaveChanges();
        return true;
    }
}
