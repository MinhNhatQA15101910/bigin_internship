using Product.Api.Helpers;
using Product.Api.Interfaces;
using Product.Api.Repositories;

namespace Product.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));

        return services;
    }
}
