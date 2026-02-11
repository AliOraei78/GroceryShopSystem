using GroceryShopSystem.Application.Features.Products.DTOs;
using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShopSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository repository)
    {
        _productRepository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        var dtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price.Amount,
            Category = p.Category,
            Stock = p.Stock
        });
        return Ok(dtos);
    }
}