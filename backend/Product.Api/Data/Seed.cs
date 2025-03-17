using System.Text.Json;
using Product.Api.Interfaces;

namespace Product.Api.Data;

public class Seed
{
    public static async Task SeedProductsAsync(IProductRepository productRepository)
    {
        var productDocuments = await productRepository.GetAllProductsAsync();
        if (productDocuments.Count != 0) return;

        var productData = await File.ReadAllTextAsync("Data/ProductSeedData.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var products = JsonSerializer.Deserialize<List<Models.Product>>(productData, options);
        if (products == null) return;

        foreach (var product in products)
        {
            Console.WriteLine($"Adding product: {product.ProductName}");
            await productRepository.AddProductAsync(product);
        }
    }
}
