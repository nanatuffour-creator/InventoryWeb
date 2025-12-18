using System;
using System.ComponentModel.DataAnnotations;
using InventoryWeb.Enum;

namespace InventoryWeb.Dto;

public class UserDto
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
}
