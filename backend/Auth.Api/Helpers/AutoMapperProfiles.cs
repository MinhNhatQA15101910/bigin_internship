using Auth.Api.Dtos;
using Auth.Api.Models;
using AutoMapper;

namespace Auth.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.Roles,
                opt => opt.MapFrom(
                    src => src.UserRoles.Select(ur => ur.Role.Name).ToList()
                )
            );
        CreateMap<RegisterDto, User>();
    }
}
