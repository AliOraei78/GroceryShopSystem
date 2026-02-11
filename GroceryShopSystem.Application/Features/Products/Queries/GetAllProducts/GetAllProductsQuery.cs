// Application/Features/Products/Queries/GetAllProducts/GetAllProductsQuery.cs
using GroceryShopSystem.Application.Features.Products.DTOs;
using MediatR;

namespace GroceryShopSystem.Application.Features.Products.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>> { }