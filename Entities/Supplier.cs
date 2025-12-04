using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryWeb.Entities;

public class Supplier
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Email { get; set; }
    [Required]
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
