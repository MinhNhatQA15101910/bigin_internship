using SharedKernel;
using SharedKernel.DTOs;
using SharedKernel.Params;

namespace Application.Queries.Products;

public record GetProductsQuery(ProductParams ProductParams) : IQuery<PagedList<ProductDto>>;
