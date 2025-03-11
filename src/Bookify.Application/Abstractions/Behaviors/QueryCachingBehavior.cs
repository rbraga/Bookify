﻿using Bookify.Application.Abstractions.Cache;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors;

internal sealed class QueryCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheQuery
    where TResponse : Result
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;

    public QueryCachingBehavior(
        ICacheService cacheService, 
        ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        TResponse? cacheResult = await _cacheService.GetAsync<TResponse>(
            request.CacheKey, 
            cancellationToken);

        string name = typeof(TRequest).Name;
        if (cacheResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);

            return cacheResult;
        }

        _logger.LogInformation("Cache miss for {Query}", name);

        var result = await next();

        if (result.IsSuccess)
        {
            await _cacheService.SetAsync(request.CacheKey, result, request.CacheExpiration, cancellationToken);
        }

        return result;
    }
}
