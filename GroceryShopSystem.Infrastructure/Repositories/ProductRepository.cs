using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Core.Entities;
using GroceryShopSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GroceryShopSystem.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new Exception("No product found");
        }
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
}