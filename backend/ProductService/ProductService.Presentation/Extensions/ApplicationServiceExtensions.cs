using ProductService.Core.Domain.Repositories;
using ProductService.Infrastructure.Configuration;
using ProductService.Infrastructure.Persistence.Repositories;

namespace ProductService.Presentation.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        // Options pattern
        services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));

        // Register services
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
