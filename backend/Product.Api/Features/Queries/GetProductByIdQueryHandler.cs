using AutoMapper;
using MediatR;
using Product.Api.Dtos;
using Product.Api.Interfaces;
using Product.Api.Models;

namespace Product.Api.Features.Queries;

public class GetProductByIdQueryHandler(
    IProductRepository productRepository,
    IMapper mapper
) : IRequestHandler<GetProductByIdQuery, MediatRResponse<ProductDto>>
{
    public async Task<MediatRResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.Id);
        if (product == null)
        {
            return MediatRResponse<ProductDto>.Failure(404, "Product not found");
        }

        return MediatRResponse<ProductDto>.Success(mapper.Map<ProductDto>(product));
    }
}
