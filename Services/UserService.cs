using System.Security.Cryptography;
using InventoryWeb.Data;
using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Enum;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace InventoryWeb.Services;

public class UserService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

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

        var user = new UserEntities
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            ConfirmPassword = dto.ConfirmPassword,
            Role = dto.Role,
            CreatedAt = dto.CreatedAt
        };

        // byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        // string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //     password: user.Password!,
        //     salt: salt!,
        //     prf: KeyDerivationPrf.HMACSHA512,
        //     iterationCount: 100000,
        //     numBytesRequested: 512 / 8
        // ));

        // Store salt:hash format
        // string saltHash = Convert.ToBase64String(salt) + ":" + hash;
        // user.Password = saltHash;
        // user.ConfirmPassword = saltHash;
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

    public UserEntities? VerifyUser(string email, string password)
    {
        var user = _context.User.FirstOrDefault(u => u.Email == email);
        if (user == null) return null;

        // var parts = user.Password!.Split(':');
        // if (parts.Length != 2) return null;

        // byte[] salt = Convert.FromBase64String(parts[0]);
        // string storedHash = parts[1];

        // string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //     password: password,
        //     salt: salt,
        //     prf: KeyDerivationPrf.HMACSHA512,
        //     iterationCount: 100000,
        //     numBytesRequested: 512 / 8
        // ));

        // // Use constant-time comparison for security
        // if (CryptographicOperations.FixedTimeEquals(
        //     Convert.FromBase64String(storedHash),
        //     Convert.FromBase64String(enteredHash)))
        // {
        //     return user;
        // }
        return user;
    }


    public IReadOnlyList<UserEntities> GetAllUsers()
    {
        return [.. _context.User];
    }
}
