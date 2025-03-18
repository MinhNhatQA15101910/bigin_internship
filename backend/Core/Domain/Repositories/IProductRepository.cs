using Domain.Entities;
using SharedKernel;
using SharedKernel.Params;

namespace Domain.Repositories;

public interface IProductRepository
{
    Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);
    Task<Product?> GetProductByNameAsync(string name);
}
