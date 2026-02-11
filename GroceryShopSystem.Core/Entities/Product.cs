using GroceryShopSystem.Core.ValueObjects;

namespace GroceryShopSystem.Core.Entities;

public class Product
{
    public Guid Id { get; private set; }  // Entity identity
    public string Name { get; private set; }
    public Money Price { get; private set; }  // Value Object
    public string Category { get; private set; }
    public int Stock { get; private set; }

    // Aggregate Root – Stock is only accessible through this class
    private Product() { } // For EF Core

    public Product(string name, Money price, string category, int initialStock)
    {
        Id = Guid.NewGuid();
        Name = !string.IsNullOrWhiteSpace(name)
            ? name
            : throw new ArgumentException("Name is required");

        Price = price ?? throw new ArgumentNullException(nameof(price));

        Category = !string.IsNullOrWhiteSpace(category)
            ? category
            : throw new ArgumentException("Category is required");

        Stock = initialStock >= 0
            ? initialStock
            : throw new ArgumentException("Stock cannot be negative");
    }

    // Domain behavior
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");

        if (quantity > Stock)
            throw new InvalidOperationException(
                $"Insufficient stock. Available: {Stock}");

        Stock -= quantity;

        // You can publish a Domain Event here (in the future)
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");

        Stock += quantity;
    }
}