// AddProductCommand.cs
namespace GroceryShopSystem.Application.Features.Products.Commands;

public class AddProductCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public int InitialStock { get; set; }
}