namespace ProductService.Core.Application.Commands;

public record DeleteProductCommand(string Id) : ICommand<bool>;
