using Application.DTOs.Products;
using SharedKernel.DTOs;

namespace Application.Commands.Products;

public record AddProductCommand(
    AddUpdateProductDto AddProductDto
) : ICommand<ProductDto>;
