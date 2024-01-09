using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
