using InventoryWeb.Data;
using InventoryWeb.Entities;
using InventoryWeb.Enum;

namespace InventoryWeb.Services;

public class UserService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public void AddUser(UserEntities user)
    {
        var lastUser = _context.User
        .OrderByDescending(u => u.UserId)
        .FirstOrDefault();

        int nextNumber = 1;

        if (lastUser != null)
        {
            // Extract numeric part from lastUser.UserId (e.g. ADMIN005 → 5)
            string numericPart = new([.. lastUser.UserId!.SkipWhile(c => !char.IsDigit(c))]);
            if (int.TryParse(numericPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        user.UserId = GenerateUserId(nextNumber, user.Role!.Value);
        _context.User.Add(user);
        _context.SaveChanges();
    }

    public string GenerateUserId(int nextNumber, Roles role)
    {
        return role switch
        {
            Roles.Admin => $"ADMIN{nextNumber:D3}",// ADMIN001, ADMIN002
            Roles.Staff => $"STAFF{nextNumber:D3}",// STAFF001, STAFF002
            Roles.Manager => $"MANAGER{nextNumber:D3}",// MANAGER001, MANAGER002
            _ => $"USER{nextNumber:D3}",// fallback
        };
    }

    public UserEntities? VerifyUser(string email, string password)
    {
        return _context.User.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public IReadOnlyList<UserEntities> GetAllUsers()
    {
        return [.. _context.User];
    }
}
