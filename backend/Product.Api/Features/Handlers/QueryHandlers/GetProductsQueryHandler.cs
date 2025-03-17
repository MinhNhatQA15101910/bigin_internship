using AutoMapper;
using Product.Api.Dtos;
using Product.Api.Features.Queries;
using Product.Api.Helpers;
using Product.Api.Interfaces;

namespace Product.Api.Features.Handlers.QueryHandlers;

public class GetProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper
) : IQueryHandler<GetProductsQuery, PagedList<ProductDto>>
{
    public async Task<PagedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsAsync(request);
        var productDtos = PagedList<ProductDto>.Map(products, mapper);

        return productDtos;
    }
}
