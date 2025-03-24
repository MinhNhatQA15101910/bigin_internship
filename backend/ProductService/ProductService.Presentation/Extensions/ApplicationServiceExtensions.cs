using FluentValidation;
using MediatR;
using ProductService.Core.Application;
using ProductService.Core.Application.Behaviors;
using ProductService.Core.Domain.Repositories;
using ProductService.Infrastructure.Configuration;
using ProductService.Infrastructure.Persistence.Repositories;
using ProductService.Presentation.Middlewares;

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

        // Middleware
        services.AddScoped<ExceptionHandlingMiddleware>();

        // MediatR
        var applicationAssembly = typeof(AssemblyReference).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}
