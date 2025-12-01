using System;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Services;

public class ProductServices(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public void AddProduct(ProductsDto dto)
    {
        var product = new ProductsEntities
        {
            ProductName = dto.ProductName,
            ProductImage = dto.ProductImage,
            ProductDescription = dto.ProductDescription,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId
        };

        _context.Add(product);
        _context.SaveChanges();
    }

    public IEnumerable<ProductsEntities> GetProducts()
    {
        return [.. _context.Products];
    }

    public bool DeleteProduct(string productName)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.ProductName == productName);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateProduct(ProductsDto dto)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.ProductName == dto.ProductName);

        if (product is null) return false;

        var categoryExists = _context.Category.Any(c => c.CategoryId == dto.CategoryId);
        if (!categoryExists) return false;

        product.ProductDescription = dto.ProductDescription;
        product.ProductImage = dto.ProductImage;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.CategoryId = dto.CategoryId;

        _context.SaveChanges();
        return true;
    }


}
