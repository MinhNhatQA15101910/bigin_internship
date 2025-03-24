namespace ProductService.Infrastructure.Configuration;

public class ProductDatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string ProductsCollectionName { get; set; }
}
