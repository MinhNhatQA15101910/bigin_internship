using AutoMapper;
using FacilityService.Core.Domain.Entities;
using SharedKernel.DTOs;

namespace FacilityService.Core.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Active, ActiveDto>();
        CreateMap<Location, LocationDto>();
        CreateMap<TimePeriod, TimePeriodDto>();
        CreateMap<FacilityPhoto, PhotoDto>();
        CreateMap<Photo, PhotoDto>();
        CreateMap<ManagerInfo, ManagerInfoDto>();
        CreateMap<Facility, FacilityDto>();
    }
}
