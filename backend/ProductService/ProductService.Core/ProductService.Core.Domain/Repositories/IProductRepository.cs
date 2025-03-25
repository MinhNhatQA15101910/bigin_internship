using ProductService.Core.Domain.Entities;

namespace ProductService.Core.Domain.Repositories;

public interface IProductRepository
{
    Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByNameAsync(string productName, CancellationToken cancellationToken = default);
}
