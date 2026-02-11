using GroceryShopSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShopSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }
}