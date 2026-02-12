using System;
using System.Threading.Tasks;

namespace GroceryShopSystem.Application.Features.Products;

public class SendProductAddedEmailJob
{
    public async Task Execute(Guid productId)
    {
        Console.WriteLine($"[Background Job] Sending email notification for new product: {productId}");

        await Task.CompletedTask;
    }
}