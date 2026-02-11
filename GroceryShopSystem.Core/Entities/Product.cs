namespace GroceryShopSystem.Core.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }
    public int Stock { get; private set; }

    // Constructor for creation
    public Product(string name, decimal price, string category, int stock)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price > 0 ? price : throw new ArgumentException("Price must be positive");
        Category = category ?? throw new ArgumentNullException(nameof(category));
        Stock = stock >= 0 ? stock : throw new ArgumentException("Stock cannot be negative");
    }

    // Business method example
    public void ReduceStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Insufficient stock");

        Stock -= quantity;
    }
}