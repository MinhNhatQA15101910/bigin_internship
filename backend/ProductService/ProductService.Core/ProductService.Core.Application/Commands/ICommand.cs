using MediatR;

namespace ProductService.Core.Application.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
