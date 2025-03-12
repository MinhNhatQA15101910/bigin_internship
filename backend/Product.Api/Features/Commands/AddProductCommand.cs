using MediatR;
using Product.Api.Dtos;
using Product.Api.Models;

namespace Product.Api.Features.Commands;

public record AddProductCommand(
    string ProductName,
    string Description,
    decimal Price,
    int StockQuantity
) : IRequest<MediatRResponse<ProductDto>>;
