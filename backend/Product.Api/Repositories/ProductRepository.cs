using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product.Api.Helpers;
using Product.Api.Interfaces;

namespace Product.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Models.Product> _products;

    public ProductRepository(IOptions<ProductDatabaseSettings> productDatabaseSettings)
    {
        var mongoClient = new MongoClient(productDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productDatabaseSettings.Value.DatabaseName);
        _products = mongoDatabase.GetCollection<Models.Product>(productDatabaseSettings.Value.ProductsCollectionName);
    }
}
