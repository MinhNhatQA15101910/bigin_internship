using FacilityService.Core.Domain.Entities;
using FacilityService.Core.Domain.Repositories;
using FacilityService.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FacilityService.Infrastructure.Persistence.Repositories;

public class FacilityRepository : IFacilityRepository
{
    private readonly IMongoCollection<Facility> _facilities;

    public FacilityRepository(
        IOptions<FacilityDatabaseSettings> facilityDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            facilityDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            facilityDatabaseSettings.Value.DatabaseName);

        _facilities = mongoDatabase.GetCollection<Facility>(
            facilityDatabaseSettings.Value.FacilitiesCollectionName);
    }

    public async Task AddFacilityAsync(Facility facility, CancellationToken cancellationToken = default)
    {
        await _facilities.InsertOneAsync(facility, cancellationToken: cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await _facilities.Find(_ => true).AnyAsync(cancellationToken);
    }

    public async Task<Facility?> GetFacilityByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _facilities.Find(facility => facility.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task InsertManyAsync(IEnumerable<Facility> facilities, CancellationToken cancellationToken = default)
    {
        return _facilities.InsertManyAsync(facilities, cancellationToken: cancellationToken);
    }
}
