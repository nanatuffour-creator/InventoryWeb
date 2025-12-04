using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;

namespace InventoryWeb.Services;

public class Categories(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public void AddCategory(CategoryDto dto)
    {
        var category = new CategoryEntities
        {
            Name = dto.Name
        };

        _context.Category.Add(category);
        _context.SaveChanges();
    }

    public IEnumerable<CategoryEntities> GetCategories()
    {
        return [.. _context.Category];
    }

    public bool DeleteCategory(string catName)
    {
        var categoryName = _context.Category.FirstOrDefault(c => c.Name == catName);

        if (categoryName == null)
        {
            return false;
        }
        _context.Category.Remove(categoryName);
        _context.SaveChanges();
        return true;
    }
}
