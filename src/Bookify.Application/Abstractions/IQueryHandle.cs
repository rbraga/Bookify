using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Abstractions;

public interface IQueryHandle<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
