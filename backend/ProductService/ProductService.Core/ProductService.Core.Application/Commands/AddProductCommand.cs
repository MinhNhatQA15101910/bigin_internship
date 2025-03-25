using ProductService.Core.Application.DTOs;
using SharedKernel.DTOs;

namespace ProductService.Core.Application.Commands;

public record AddProductCommand(AddUpdateProductDto AddProductDto) : ICommand<ProductDto>;
