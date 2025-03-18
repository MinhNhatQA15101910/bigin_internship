using SharedKernel.DTOs;

namespace Application.Queries.Products;

public record GetProductByIdQuery(string Id) : IQuery<ProductDto>;
