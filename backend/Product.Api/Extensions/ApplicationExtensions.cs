using FluentValidation;
using Product.Api.Features.Commands;
using Product.Api.Helpers;
using Product.Api.Interfaces;
using Product.Api.Repositories;

namespace Product.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        // Repositories
        services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));
        services.AddScoped<IProductRepository, ProductRepository>();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
