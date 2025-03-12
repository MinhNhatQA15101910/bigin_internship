namespace Product.Api.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Models.Product product);
    Task<Models.Product> GetProductByIdAsync(string id);
    Task<List<Models.Product>> GetProductsAsync();
}
