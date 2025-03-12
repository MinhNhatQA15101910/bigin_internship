using AutoMapper;
using Product.Api.Dtos;
using Product.Api.Features.Commands;

namespace Product.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Models.Product, ProductDto>();
        CreateMap<AddProductCommand, Models.Product>();
    }
}
