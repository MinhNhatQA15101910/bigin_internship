using MediatR;
using ProductService.Core.Application.Queries;

namespace ProductService.Core.Application.Handlers.QueryHandlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
