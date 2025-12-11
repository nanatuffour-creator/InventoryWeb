using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryWeb.Dto;

public class SupplierDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class SuppliersDto
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Email { get; set; }
    [Required]
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}