using AutoMapper;
using ProductService.Core.Domain.Entities;
using SharedKernel.DTOs;

namespace ProductService.Core.Application.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductDto>();
    }
}
