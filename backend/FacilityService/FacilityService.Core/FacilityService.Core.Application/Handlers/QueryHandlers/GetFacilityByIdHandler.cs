using AutoMapper;
using FacilityService.Core.Application.Queries;
using FacilityService.Core.Domain.Repositories;
using SharedKernel.DTOs;

namespace FacilityService.Core.Application.Handlers.QueryHandlers;

public class GetFacilityByIdHandler(
    IFacilityRepository facilityRepository,
    IMapper mapper
) : IQueryHandler<GetFacilityByIdQuery, FacilityDto>
{
    public async Task<FacilityDto> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
    {
        var facility = await facilityRepository.GetFacilityByIdAsync(request.Id, cancellationToken)
            ?? throw new($"Facility with id {request.Id} not found.");

        return mapper.Map<FacilityDto>(facility);
    }
}
