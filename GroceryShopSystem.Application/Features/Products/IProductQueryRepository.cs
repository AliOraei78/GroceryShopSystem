using GroceryShopSystem.Core.Entities;

public interface IProductQueryRepository
{
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
}