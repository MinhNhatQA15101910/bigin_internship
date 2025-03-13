using MediatR;
using Product.Api.Features.Queries;

namespace Product.Api.Features.Handlers.QueryHandlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
