using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;

namespace InventoryWeb.Services;

public class CustomerService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public IEnumerable<Customer> GetCustomer()
    {
        return [.. _context.Customers];
    }

    public bool UpdateCustomer(CustomerDto dto, string name)
    {
        var customer = _context.Customers.FirstOrDefault(
            p => p.Name == name
        );

        if (customer is null)
            return false;

        customer.Name = dto.Name;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;
        _context.SaveChanges();

        return true;
    }

    public bool DeleteCustomer(int id)
    {
        var customer = _context.Customers.FirstOrDefault(
            p => p.Id == id
        );
        if (customer is null)
            return false;

        _context.Remove(customer);
        _context.SaveChanges();
        return true;
    }
}
