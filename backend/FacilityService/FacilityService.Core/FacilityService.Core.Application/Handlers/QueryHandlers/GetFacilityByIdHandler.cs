using System.Text.Json;
using AutoMapper;
using FacilityService.Core.Application.Queries;
using FacilityService.Core.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using SharedKernel.DTOs;
using SharedKernel.Exceptions;

namespace FacilityService.Core.Application.Handlers.QueryHandlers;

public class GetFacilityByIdHandler(
    IFacilityRepository facilityRepository,
    IMapper mapper,
    IDistributedCache cache
) : IQueryHandler<GetFacilityByIdQuery, FacilityDto>
{
    public async Task<FacilityDto> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
    {
        var facility = await facilityRepository.GetFacilityByIdAsync(request.Id, cancellationToken)
                ?? throw new FacilityNotFoundException(request.Id);

        return mapper.Map<FacilityDto>(facility);
    }

    // public async Task<FacilityDto> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
    // {
    //     var cacheKey = $"facilities/{request.Id}";
    //     FacilityDto? facilityDto;

    //     var cachedData = await cache.GetStringAsync(cacheKey, token: cancellationToken);
    //     if (!string.IsNullOrEmpty(cachedData))
    //     {
    //         facilityDto = JsonSerializer.Deserialize<FacilityDto>(cachedData) ?? new FacilityDto();
    //     }
    //     else
    //     {
    //         var facility = await facilityRepository.GetFacilityByIdAsync(request.Id, cancellationToken)
    //             ?? throw new FacilityNotFoundException(request.Id);

    //         facilityDto = mapper.Map<FacilityDto>(facility);

    //         var serializedData = JsonSerializer.Serialize(facilityDto);
    //         await cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
    //         {
    //             SlidingExpiration = TimeSpan.FromMinutes(5)
    //         }, token: cancellationToken);
    //     }

    //     return facilityDto;
    // }
}
