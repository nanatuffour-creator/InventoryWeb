using System;
using System.ComponentModel.DataAnnotations;
using InventoryWeb.Enum;
using InventoryWeb.Entities;

namespace InventoryWeb.Entities;

public class UserEntities
{
    [Key]
    public string? UserId { get; set; }

    [Required, MaxLength(50)]
    public string? FirstName { get; set; }

    [Required, MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required, MinLength(8)]
    public string? Password { get; set; }

    [Required, MinLength(8)]
    public string? ConfirmPassword { get; set; }

    public Roles? Role { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<Invoice>? Invoices { get; set; }
}
