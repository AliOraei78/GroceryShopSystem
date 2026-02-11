using MediatR;
using GroceryShopSystem.Application.Features.Products.Commands;
using GroceryShopSystem.Application.Features.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShopSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id }, null);
    }
}