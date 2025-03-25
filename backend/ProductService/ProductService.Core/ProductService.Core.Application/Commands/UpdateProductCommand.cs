using ProductService.Core.Application.DTOs;

namespace ProductService.Core.Application.Commands;

public record UpdateProductCommand(string Id, UpdateProductDto UpdateProductDto) : ICommand<bool>;
