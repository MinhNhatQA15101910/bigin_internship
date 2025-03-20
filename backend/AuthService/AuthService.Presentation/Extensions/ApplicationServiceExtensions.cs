using Application.Behaviors;
using Application.Interfaces;
using Application.Services;
using Configuration;
using Domain.Repositories.MongoDb;
using Domain.Repositories.Sqlite;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Presentation.Middlewares;

namespace AuthService.Presentation.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        // Options pattern
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.Configure<EmailSenderSettings>(config.GetSection("EmailSenderSettings"));
        services.Configure<MongoDbSettings>(config.GetSection("MongoDbSettings"));

        // Database and repositories
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(
                config.GetConnectionString("DefaultConnection"),
                options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            );
        });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddSingleton(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var settings = sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value;
            return new MongoDbTransactionManager(mongoClient);
        });

        // Services
        services.AddSingleton<PincodeStore>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();

        // Middleware
        services.AddScoped<ExceptionHandlingMiddleware>();

        // MediatR
        var applicationAssembly = typeof(Application.AssemblyReference).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(applicationAssembly);

        // Others
        services.AddAutoMapper(applicationAssembly);

        return services;
    }
}

internal class ProductDatabaseSettings
{
}