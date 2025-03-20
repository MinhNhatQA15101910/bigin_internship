using Domain.Entities;
using MongoDB.Driver;
using SharedKernel;
using SharedKernel.Params;

namespace Domain.Repositories.MongoDb;

public interface IProductRepository
{
    Task AddProductAsync(Product product, IClientSessionHandle? session = null, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);
    Task<Product?> GetProductByNameAsync(string name);
}

