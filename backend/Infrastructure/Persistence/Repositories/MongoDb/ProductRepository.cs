using Configuration;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SharedKernel;
using SharedKernel.Params;

namespace Persistence.Repositories.MongoDb;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _productsCollection = mongoDatabase.GetCollection<Product>(mongoDbSettings.Value.ProductsCollectionName);
    }

    public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _productsCollection.InsertOneAsync(product, cancellationToken: cancellationToken);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _productsCollection
            .Find(product => product.Id.ToString() == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        return await _productsCollection
            .Find(product => product.ProductName == name)
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<Product>> GetProductsAsync(ProductParams productParams)
    {
        var filterBuilder = Builders<Product>.Filter;
        var filters = new List<FilterDefinition<Product>>();

        // Filter by product name
        if (productParams.ProductName != null)
        {
            var productNameFilter = filterBuilder.Regex(
                p => p.ProductName,
                new BsonRegularExpression(productParams.ProductName, "i")
            );
            filters.Add(productNameFilter);
        }

        filters.Add(filterBuilder.Gte(p => p.Price, productParams.MinPrice));
        filters.Add(filterBuilder.Lte(p => p.Price, productParams.MaxPrice));

        var finalFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

        var sortBuilder = Builders<Product>.Sort;
        var sortDefinition = productParams.OrderBy switch
        {
            "productName" => productParams.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.ProductName)
                : sortBuilder.Descending(u => u.ProductName),
            "price" => productParams.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.Price)
                : sortBuilder.Descending(u => u.Price),
            "updatedAt" => productParams.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.UpdatedAt)
                : sortBuilder.Descending(u => u.UpdatedAt),
            _ => sortBuilder.Ascending(u => u.UpdatedAt)
        };

        var productsQuery = _productsCollection.Find(finalFilter).Sort(sortDefinition);

        return await PagedList<Product>.CreateAsync(
            productsQuery,
            productParams.PageNumber,
            productParams.PageSize
        );
    }
}
