using FacilityService.Core.Domain.Entities;

namespace FacilityService.Core.Domain.Repositories;

public interface IFacilityRepository
{
    Task AddFacilityAsync(Facility facility, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    Task InsertManyAsync(IEnumerable<Facility> facilities, CancellationToken cancellationToken = default);
}
