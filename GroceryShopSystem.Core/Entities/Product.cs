// Core/Entities/Product.cs
using GroceryShopSystem.Core.ValueObjects;
using System;
using Ardalis.GuardClauses;

namespace GroceryShopSystem.Core.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public string Category { get; private set; }
    public int Stock { get; private set; }

    private Product() { } // EF Core

    public Product(string name, Money price, string category, int initialStock)
    {
        Id = Guid.NewGuid();
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

        // Fix: Guard clauses throw automatically. No need for ternary logic.
        Guard.Against.NegativeOrZero(price.Amount, nameof(price));
        Price = price;

        Category = Guard.Against.NullOrWhiteSpace(category, nameof(category));
        Stock = Guard.Against.Negative(initialStock, nameof(initialStock));

        // Domain event (Future use)
        // DomainEvents.Raise(new ProductCreatedEvent(Id));
    }

    public void ReduceStock(int quantity)
    {
        Guard.Against.NegativeOrZero(quantity, nameof(quantity));

        if (quantity > Stock)
            throw new InsufficientStockException($"Insufficient stock. Available: {Stock}, Requested: {quantity}");

        Stock -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        Guard.Against.NegativeOrZero(quantity, nameof(quantity));
        Stock += quantity;
    }

    public void UpdatePrice(Money newPrice)
    {
        Guard.Against.NegativeOrZero(newPrice.Amount, nameof(newPrice));
        Price = newPrice;
    }
}

// Add this class to your project (usually in Core/Exceptions)
public class InsufficientStockException : Exception
{
    public InsufficientStockException(string message) : base(message) { }
}