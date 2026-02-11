// Application/Features/Products/Commands/AddProduct/AddProductCommandHandler.cs
using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Core.Entities;
using GroceryShopSystem.Core.ValueObjects;
using MediatR;

namespace GroceryShopSystem.Application.Features.Products.Commands;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public AddProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            new Money(request.Price, "IRR"), // فرض ارز IRR
            request.Category,
            request.InitialStock
        );

        await _repository.AddAsync(product);

        return product.Id;
    }
}