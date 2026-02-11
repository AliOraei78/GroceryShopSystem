// Application/Features/Products/Validators/ProductValidator.cs
using GroceryShopSystem.Core.Entities;

public class ProductValidator: IValidator<Product>
{
    public void Validate(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Name is required");

        if (product.Price <= 0)
            throw new ArgumentException("Price must be positive");
    }
}