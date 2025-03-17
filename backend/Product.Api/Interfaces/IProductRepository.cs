using Product.Api.Dtos;
using Product.Api.Features.Queries;
using Product.Api.Helpers;

namespace Product.Api.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Models.Product product, CancellationToken cancellationToken = default);
    Task<List<Models.Product>> GetAllProductsAsync();
    Task<Models.Product> GetProductByIdAsync(string id);
    Task<PagedList<Models.Product>> GetProductsAsync(GetProductsQuery getProductsQuery);
    Task<Models.Product?> GetProductByNameAsync(string name);
}
