using Application.Queries.Products;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories.MongoDb;
using SharedKernel.DTOs;

namespace Application.Handlers.QueryHandlers.Products;

public class GetProductByIdHandler(
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
