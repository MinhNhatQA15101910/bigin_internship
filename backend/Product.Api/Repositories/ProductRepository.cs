using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product.Api.Helpers;
using Product.Api.Interfaces;

namespace Product.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Models.Product> _productsCollection;

    public ProductRepository(IOptions<ProductDatabaseSettings> productDatabaseSettings)
    {
        var mongoClient = new MongoClient(productDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productDatabaseSettings.Value.DatabaseName);
        _productsCollection = mongoDatabase.GetCollection<Models.Product>(productDatabaseSettings.Value.ProductsCollectionName);
    }

    public async Task AddProductAsync(Models.Product product)
    {
        await _productsCollection.InsertOneAsync(product);
    }

    public async Task<Models.Product> GetProductByIdAsync(string id)
    {
        return await _productsCollection.Find(product => product.Id.ToString() == id).FirstOrDefaultAsync();
    }

    public async Task<List<Models.Product>> GetProductsAsync()
    {
        return await _productsCollection.Find(_ => true).ToListAsync();
    }
}
