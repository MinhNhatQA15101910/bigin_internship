using MediatR;

namespace Product.Api.Features.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
