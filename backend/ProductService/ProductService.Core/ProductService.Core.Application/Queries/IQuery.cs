using MediatR;

namespace ProductService.Core.Application.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
