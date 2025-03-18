using Product.Api.Dtos;

namespace Product.Api.Features.Commands;

public record AddProductCommand(
    AddUpdateProductDto AddProductDto
) : ICommand<ProductDto>;
