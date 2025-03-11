using AutoMapper;
using Product.Api.Dtos;

namespace Product.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Models.Product, ProductDto>();
        CreateMap<AddProductDto, Models.Product>();
    }
}
