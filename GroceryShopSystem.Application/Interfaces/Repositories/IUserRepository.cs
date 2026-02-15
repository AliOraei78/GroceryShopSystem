using GroceryShopSystem.Core.Security;
using GroceryShopSystem.Core.Interfaces;

namespace GroceryShopSystem.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}