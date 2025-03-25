using AutoMapper;
using ProductService.Core.Application.DTOs;
using ProductService.Core.Domain.Entities;
using SharedKernel.DTOs;

namespace ProductService.Core.Application.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<AddProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
