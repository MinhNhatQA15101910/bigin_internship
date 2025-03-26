using System.Text.Json;
using FacilityService.Core.Domain.Entities;
using FacilityService.Core.Domain.Repositories;

namespace FacilityService.Infrastructure.Persistence;

public class Seed
{
    public static async Task SeedFacilitiesAsync(
        IFacilityRepository facilityRepository
    )
    {
        if (await facilityRepository.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync(
            "../FacilityService.Infrastructure/FacilityService.Infrastructure.Persistence/Data/FacilitySeedData.json"
        );

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var facilities = JsonSerializer.Deserialize<List<Facility>>(userData, options);

        if (facilities == null) return;

        await facilityRepository.InsertManyAsync(facilities);
    }
}
