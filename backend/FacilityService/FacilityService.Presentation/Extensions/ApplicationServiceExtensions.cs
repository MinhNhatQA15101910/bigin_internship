using FacilityService.Core.Domain.Repositories;
using FacilityService.Infrastructure.Configuration;
using FacilityService.Infrastructure.Persistence.Repositories;

namespace FacilityService.Presentation.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        // Options pattern
        services.Configure<FacilityDatabaseSettings>(config.GetSection(nameof(FacilityDatabaseSettings)));

        // Repositories
        services.AddScoped<IFacilityRepository, FacilityRepository>();

        return services;
    }
}
