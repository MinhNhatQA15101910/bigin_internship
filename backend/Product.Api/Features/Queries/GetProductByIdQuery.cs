using Product.Api.Dtos;

namespace Product.Api.Features.Queries;

public record GetProductByIdQuery(string Id) : IQuery<ProductDto>;
