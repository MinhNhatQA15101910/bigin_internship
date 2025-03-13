using MediatR;

namespace Product.Api.Features.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
