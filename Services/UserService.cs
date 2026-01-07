using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Enum;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace InventoryWeb.Services;

public class UserService(DatabaseContext context, IConfiguration configuration)
{
    private readonly DatabaseContext _context = context;
    private static readonly UserEntities users = new();
    public void AddUser(UserDto dto)
    {
        var lastUser = _context.User
        .OrderByDescending(u => u.UserId)
        .FirstOrDefault();

        int nextNumber = 1;

        if (lastUser != null)
        {
            string numericPart = new([.. lastUser.UserId!.SkipWhile(c => !char.IsDigit(c))]);
            if (int.TryParse(numericPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }
        var hashPassword = new PasswordHasher<UserEntities>().HashPassword(users, dto.Password!);
        var hashPasswords = new PasswordHasher<UserEntities>().HashPassword(users, dto.ConfirmPassword!);
        var user = new UserEntities
        {
            // UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = hashPassword,
            ConfirmPassword = hashPasswords,
            Role = dto.Role,
            CreatedAt = dto.CreatedAt
        };

        user.UserId = GenerateUserId(nextNumber, user.Role!.Value);
        _context.User.Add(user);
        _context.SaveChanges();
    }

    public string GenerateUserId(int nextNumber, Roles role)
    {
        return role switch
        {
            Roles.Admin => $"ADMIN{nextNumber:D3}",
            Roles.Staff => $"STAFF{nextNumber:D3}",
            Roles.Manager => $"MANAGER{nextNumber:D3}",
            _ => $"USER{nextNumber:D3}",
        };
    }

    public async Task<string?> VerifyUser(LoginDto login)
    {
        var user = _context.User.FirstOrDefault(u => u.Email == login.Email);
        if (user == null) return "User not Found";

        if (new PasswordHasher<UserEntities>().VerifyHashedPassword(users, user.Password!, login.Password!) == PasswordVerificationResult.Failed)
            return "Invalid Password or Username";

        var claim = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName!),
            new Claim(ClaimTypes.Name, user.LastName!),
            new Claim(ClaimTypes.NameIdentifier, user.UserId!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claim,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }


    public IReadOnlyList<UserEntities> GetAllUsers()
    {
        return [.. _context.User];
    }
}
