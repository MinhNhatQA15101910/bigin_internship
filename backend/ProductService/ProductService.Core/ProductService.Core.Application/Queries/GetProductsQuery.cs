using ProductService.Core.Domain.Entities;
using SharedKernel;
using SharedKernel.DTOs;
using SharedKernel.Params;

namespace ProductService.Core.Application.Queries;

public record GetProductsQuery(ProductParams ProductParams) : IQuery<PagedList<ProductDto>>;
