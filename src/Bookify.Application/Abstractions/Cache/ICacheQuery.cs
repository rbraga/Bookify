using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Abstractions.Cache;

public interface ICacheQuery<TResponse> : IQuery<TResponse>, ICacheQuery;

public interface ICacheQuery
{
    string CacheKey { get; }
    TimeSpan? CacheExpiration { get; }
}
