using System;

namespace InventoryWeb.Entities;

using System.ComponentModel.DataAnnotations;

public class CategoryEntities
{
    [Key]
    public int CategoryId { get; set; }   // PK
    public string? Name { get; set; }
    public ICollection<ProductsEntities> Products { get; set; } = new List<ProductsEntities>();
}
