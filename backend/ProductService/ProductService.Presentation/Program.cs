using ProductService.Core.Domain.Repositories;
using ProductService.Infrastructure.Persistence;
using ProductService.Presentation.Extensions;
using ProductService.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var productRepository = services.GetRequiredService<IProductRepository>();

    await Seed.SeedProductsAsync(productRepository);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
