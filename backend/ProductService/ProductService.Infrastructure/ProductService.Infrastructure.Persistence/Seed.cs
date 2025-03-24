using System.Text.Json;
using ProductService.Core.Domain.Entities;
using ProductService.Core.Domain.Repositories;

namespace ProductService.Infrastructure.Persistence;

public class Seed
{
    public static async Task SeedProductsAsync(
        IProductRepository productRepository
    )
    {
        if (await productRepository.AnyAsync()) return;

        var productData = await File.ReadAllTextAsync(
            "../ProductService.Infrastructure/ProductService.Infrastructure.Persistence/Data/ProductSeedData.json"
        );

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var products = JsonSerializer.Deserialize<List<Product>>(productData, options);

        if (products == null) return;

        foreach (var product in products)
        {
            Console.WriteLine($"Adding product {product.ProductName} to the database");

            await productRepository.AddProductAsync(product);
        }
    }
}
