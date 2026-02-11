// Application/Features/Products/Queries/GetAllProducts/GetAllProductsQueryHandler.cs
using MediatR;
using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Application.Features.Products.DTOs;

namespace GroceryShopSystem.Application.Features.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price.Amount,
            Category = p.Category,
            Stock = p.Stock
        });
    }
}