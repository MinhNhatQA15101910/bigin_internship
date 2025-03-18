using Application.Queries.Products;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;
using SharedKernel.DTOs;

namespace Application.Handlers.QueryHandlers.Products;

public class GetProductsHandler(
    IProductRepository productRepository,
    IMapper mapper
) : IQueryHandler<GetProductsQuery, PagedList<ProductDto>>
{
    public async Task<PagedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsAsync(request.ProductParams);
        var productDtos = PagedList<ProductDto>.Map(products, mapper);

        return productDtos;
    }
}
