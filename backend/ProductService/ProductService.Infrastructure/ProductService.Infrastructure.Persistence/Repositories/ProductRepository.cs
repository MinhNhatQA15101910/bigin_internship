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

    public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _products.InsertOneAsync(product, cancellationToken: cancellationToken);
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return _products.Find(product => true).AnyAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _products
            .Find(product => product.Id.ToString() == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
