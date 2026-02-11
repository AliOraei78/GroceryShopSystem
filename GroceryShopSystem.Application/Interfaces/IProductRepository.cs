using GroceryShopSystem.Core.Entities;

namespace GroceryShopSystem.Application.Interfaces;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
}