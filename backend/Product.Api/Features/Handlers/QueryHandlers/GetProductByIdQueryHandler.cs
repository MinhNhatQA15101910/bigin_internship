using AutoMapper;
using Product.Api.Dtos;
using Product.Api.Exceptions;
using Product.Api.Features.Queries;
using Product.Api.Interfaces;

namespace Product.Api.Features.Handlers.QueryHandlers;

public class GetProductByIdQueryHandler(
    IProductRepository productRepository,
    IMapper mapper
) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.Id) 
            ?? throw new ProductNotFoundException(request.Id);
            
        return mapper.Map<ProductDto>(product);
    }
}
