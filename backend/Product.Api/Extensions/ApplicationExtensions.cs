using FluentValidation;
using MediatR;
using Product.Api.Features.Behaviors;
using Product.Api.Features.Commands;
using Product.Api.Helpers;
using Product.Api.Interfaces;
using Product.Api.Middlewares;
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

        // Middlewares
        services.AddScoped<ExceptionHandlingMiddleware>();

        // MediatR
        var applicationAssembly = typeof(Program).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
