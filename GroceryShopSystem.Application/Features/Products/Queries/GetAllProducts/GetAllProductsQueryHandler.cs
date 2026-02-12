using GroceryShopSystem.Application.Features.Products.DTOs;
using GroceryShopSystem.Application.Features.Products.Queries;
using GroceryShopSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;
    private readonly IDistributedCache _cache;

    public GetAllProductsQueryHandler(IProductRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = "products:all";

        var cached = await _cache.GetStringAsync(cacheKey);
        if (cached != null)
        {
            var deserialized = JsonSerializer.Deserialize<IEnumerable<ProductDto>>(cached);
            return deserialized ?? Enumerable.Empty<ProductDto>();
        }

        var products = await _repository.GetAllAsync();
        var dtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price.Amount,
            Category = p.Category,
            Stock = p.Stock
        }).ToList();

        var json = JsonSerializer.Serialize(dtos);
        await _cache.SetStringAsync(cacheKey, json, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        });

        return dtos;
    }
}