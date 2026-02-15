using GroceryShopSystem.Core.Security;
using Microsoft.EntityFrameworkCore;
using GroceryShopSystem.Core.Security;
using GroceryShopSystem.Application.Security;
using Microsoft.EntityFrameworkCore;

namespace GroceryShopSystem.Infrastructure.Persistence;

public static class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext context, IPasswordHasher passwordHasher)
    {
        if (!await context.Users.AnyAsync())
        {
            var admin = new User(
                email: "admin@grocery.com",
                passwordHash: passwordHasher.Hash("Admin123!"),
                role: "Admin"
            );

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}