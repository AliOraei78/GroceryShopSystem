// Core/Security/User.cs
namespace GroceryShopSystem.Core.Security;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; } // e.g., "Admin", "Customer"

    private User() { }

    public User(string email, string passwordHash, string role)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public bool VerifyPassword(string password, IPasswordHasher hasher)
    {
        return hasher.Verify(password, PasswordHash);
    }
}