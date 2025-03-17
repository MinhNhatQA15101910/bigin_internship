using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Product.Api.Features.Queries;
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

    public async Task AddProductAsync(Models.Product product, CancellationToken cancellationToken)
    {
        await _productsCollection.InsertOneAsync(product, cancellationToken: cancellationToken);
    }

    public async Task<List<Models.Product>> GetAllProductsAsync()
    {
        return await _productsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Models.Product> GetProductByIdAsync(string id)
    {
        return await _productsCollection
            .Find(product => product.Id.ToString() == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Models.Product?> GetProductByNameAsync(string name)
    {
        return await _productsCollection
            .Find(product => product.ProductName == name)
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<Models.Product>> GetProductsAsync(GetProductsQuery getProductsQuery)
    {
        var filterBuilder = Builders<Models.Product>.Filter;
        var filters = new List<FilterDefinition<Models.Product>>();

        // Filter by product name
        if (getProductsQuery.ProductName != null)
        {
            var productNameFilter = filterBuilder.Regex(
                p => p.ProductName,
                new BsonRegularExpression(getProductsQuery.ProductName, "i")
            );
            filters.Add(productNameFilter);
        }

        var finalFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

        var sortBuilder = Builders<Models.Product>.Sort;
        var sortDefinition = getProductsQuery.OrderBy switch
        {
            "productName" => getProductsQuery.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.ProductName)
                : sortBuilder.Descending(u => u.ProductName),
            "price" => getProductsQuery.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.Price)
                : sortBuilder.Descending(u => u.Price),
            "updatedAt" => getProductsQuery.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.UpdatedAt)
                : sortBuilder.Descending(u => u.UpdatedAt),
            _ => sortBuilder.Ascending(u => u.UpdatedAt)
        };

        var productsQuery = _productsCollection.Find(finalFilter).Sort(sortDefinition);

        return await PagedList<Models.Product>.CreateAsync(
            productsQuery,
            getProductsQuery.PageNumber,
            getProductsQuery.PageSize
        );
    }
}
