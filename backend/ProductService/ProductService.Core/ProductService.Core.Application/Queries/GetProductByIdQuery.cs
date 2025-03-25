using SharedKernel.DTOs;

namespace ProductService.Core.Application.Queries;

public record GetProductByIdQuery(string Id) : IQuery<ProductDto>;
