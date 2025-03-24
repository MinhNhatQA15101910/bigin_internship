using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Core.Domain.Entities;
using ProductService.Core.Domain.Repositories;
using ProductService.Infrastructure.Configuration;

namespace ProductService.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IOptions<ProductDatabaseSettings> productDatabaseSettings)
    {
        var mongoClient = new MongoClient(productDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productDatabaseSettings.Value.DatabaseName);
        _products = mongoDatabase.GetCollection<Product>(productDatabaseSettings.Value.ProductsCollectionName);
    }

    public async Task AddProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public Task<bool> AnyAsync()
    {
        return _products.Find(product => true).AnyAsync();
    }

    public Task<Product> GetProductByIdAsync(string id)
    {
        return _products.Find(product => product.Id.ToString() == id).FirstOrDefaultAsync();
    }
}
