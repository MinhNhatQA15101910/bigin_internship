using AutoMapper;
using ProductService.Core.Application.Queries;
using ProductService.Core.Domain.Repositories;
using SharedKernel.DTOs;
using SharedKernel.Exceptions;

namespace ProductService.Core.Application.Handlers.QueryHandlers;

public class GetProductByIdHandler(
    IProductRepository productRepository,
    IMapper mapper
) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(
            request.Id.ToString(),
            cancellationToken
        ) ?? throw new ProductNotFoundException(request.Id);

        return mapper.Map<ProductDto>(product);
    }
}
