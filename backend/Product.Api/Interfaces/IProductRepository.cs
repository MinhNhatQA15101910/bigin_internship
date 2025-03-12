namespace Product.Api.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Models.Product product, CancellationToken cancellationToken = default);
    Task<Models.Product> GetProductByIdAsync(string id);
    Task<List<Models.Product>> GetProductsAsync();
    Task<Models.Product?> GetProductByNameAsync(string name);
}
