using MediatR;
using ProductService.Core.Application.Commands;

namespace ProductService.Core.Application.Handlers.CommandHandlers;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
