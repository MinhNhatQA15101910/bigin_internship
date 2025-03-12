using MediatR;
using Product.Api.Dtos;
using Product.Api.Models;

namespace Product.Api.Features.Queries;

public record GetProductByIdQuery(string Id) : IRequest<MediatRResponse<ProductDto>>;
