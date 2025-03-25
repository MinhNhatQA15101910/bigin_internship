using ProductService.Core.Application.DTOs;
using SharedKernel.DTOs;

namespace ProductService.Core.Application.Commands;

public record AddProductCommand(AddProductDto AddProductDto) : ICommand<ProductDto>;
