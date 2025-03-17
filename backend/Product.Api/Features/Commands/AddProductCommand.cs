using Product.Api.Dtos;

namespace Product.Api.Features.Commands;

public record AddProductCommand(
    string ProductName,
    string Description,
    decimal Price,
    int StockQuantity
) : ICommand<ProductDto>;
