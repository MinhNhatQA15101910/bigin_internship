using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductService.Core.Domain.Entities;
using ProductService.Core.Domain.Repositories;
using ProductService.Infrastructure.Configuration;
using SharedKernel;
using SharedKernel.Params;

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

    public async Task<Product?> GetProductByNameAsync(string productName, CancellationToken cancellationToken = default)
    {
        return await _products
            .Find(product => product.ProductName == productName)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedList<Product>> GetProductsAsync(ProductParams productParams, CancellationToken cancellationToken = default)
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

        // Filter by category
        if (productParams.Category != null)
        {
            var categoryFilter = filterBuilder.Eq(p => p.Category, productParams.Category);
            filters.Add(categoryFilter);
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
            "category" => productParams.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.Category)
                : sortBuilder.Descending(u => u.Category),
            "price" => productParams.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.Price)
                : sortBuilder.Descending(u => u.Price),
            "updatedAt" => productParams.SortBy == "asc"
                ? sortBuilder.Ascending(u => u.UpdatedAt)
                : sortBuilder.Descending(u => u.UpdatedAt),
            _ => sortBuilder.Ascending(u => u.UpdatedAt)
        };

        var productsQuery = _products.Find(finalFilter).Sort(sortDefinition);

        return await PagedList<Product>.CreateAsync(
            productsQuery,
            productParams.PageNumber,
            productParams.PageSize,
            cancellationToken
        );
    }
}
