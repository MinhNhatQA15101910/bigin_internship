using MediatR;
using Product.Api.Features.Commands;

namespace Product.Api.Features.Handlers.CommandHandlers;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
