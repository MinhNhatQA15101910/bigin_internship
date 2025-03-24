using ProductService.Core.Domain.Entities;

namespace ProductService.Core.Domain.Repositories;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<bool> AnyAsync();
    Task<Product> GetProductByIdAsync(string id);
}
