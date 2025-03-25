using AutoMapper;
using ProductService.Core.Application.Queries;
using ProductService.Core.Domain.Repositories;
using SharedKernel;
using SharedKernel.DTOs;

namespace ProductService.Core.Application.Handlers.QueryHandlers;

public class GetProductsHandler(
    IProductRepository productRepository,
    IMapper mapper
) : IQueryHandler<GetProductsQuery, PagedList<ProductDto>>
{
    public async Task<PagedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsAsync(request.ProductParams, cancellationToken);
        var productDtos = PagedList<ProductDto>.Map(products, mapper);

        return productDtos;
    }
}
