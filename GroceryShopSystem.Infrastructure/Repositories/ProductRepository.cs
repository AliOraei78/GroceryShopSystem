using GroceryShopSystem.Application.Interfaces;
using GroceryShopSystem.Core.Entities;

namespace GroceryShopSystem.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products = new();

    public Task<Product> GetByIdAsync(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }
}