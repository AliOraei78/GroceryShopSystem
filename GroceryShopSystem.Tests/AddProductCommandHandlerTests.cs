using GroceryShopSystem.Application.Features.Products.Commands;
using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Core.Entities;
using GroceryShopSystem.Core.ValueObjects;
using Moq;
using Xunit;

namespace GroceryShopSystem.Tests.Application;

public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _repositoryMock = new();

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddProductAndReturnId()
    {
        // Arrange
        var command = new AddProductCommand
        {
            Name = "Banana",
            Price = 15_000m,
            Category = "Fruits",
            InitialStock = 50
        };

        Guid newId = Guid.Empty;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(p => newId = p.Id)
            .Returns(Task.CompletedTask);

        var handler = new AddProductCommandHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        Assert.Equal(newId, result);
        _repositoryMock.Verify(r => r.AddAsync(It.Is<Product>(p =>
            p.Name == "Banana" &&
            p.Price.Amount == 15_000m &&
            p.Category == "Fruits" &&
            p.Stock == 50
        )), Times.Once);
    }
}