using GroceryShopSystem.Core.Entities;
using GroceryShopSystem.Core.ValueObjects;
using Xunit;

namespace GroceryShopSystem.Tests.Core;

public class ProductTests
{
    [Fact]
    public void CreateProduct_WithValidData_ShouldSucceed()
    {
        // Arrange + Act
        var product = new Product(
            "Apple",
            new Money(10_000m, "IRR"),
            "Fruits",
            100
        );

        // Assert
        Assert.Equal("Apple", product.Name);
        Assert.Equal(10_000m, product.Price.Amount);
        Assert.Equal("Fruits", product.Category);
        Assert.Equal(100, product.Stock);
    }

    [Fact]
    public void CreateProduct_WithNegativePrice_ShouldThrow()
    {
        // Arrange + Act + Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Product("Apple", new Money(-1m, "IRR"), "Fruits", 100));

        // Update this string to match the exception from the Money class
        Assert.Equal("Amount cannot be negative", exception.Message);
    }

    [Fact]
    public void ReduceStock_WithSufficientQuantity_ShouldDecreaseStock()
    {
        var product = new Product("Apple", new Money(10_000m, "IRR"), "Fruits", 100);
        product.ReduceStock(30);

        Assert.Equal(70, product.Stock);
    }

    [Fact]
    public void ReduceStock_WithInsufficientQuantity_ShouldThrow()
    {
        var product = new Product("Apple", new Money(10_000m, "IRR"), "Fruits", 20);

        var exception = Assert.Throws<InvalidOperationException>(() => product.ReduceStock(30));

        Assert.Contains("Insufficient stock", exception.Message);
    }
}