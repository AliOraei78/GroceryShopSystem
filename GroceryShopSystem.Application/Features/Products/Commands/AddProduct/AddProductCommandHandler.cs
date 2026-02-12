// Application/Features/Products/Commands/AddProduct/AddProductCommandHandler.cs
using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Core.Entities;
using GroceryShopSystem.Core.ValueObjects;
using MediatR;
using Hangfire; // <--- Add this line

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
            new Money(request.Price, "IRR"),
            request.Category,
            request.InitialStock
        );

        await _repository.AddAsync(product).ConfigureAwait(false);

        BackgroundJob.Enqueue<SendProductAddedEmailJob>(job => job.Execute(product.Id));

        return product.Id;
    }
}