using System.Collections.Concurrent;
using SimpleGiphyViewer.Api.Exceptions;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Services;

public class GiphyService(IGiphyClient giphyClient, ICacheService cacheService, ILogger<GiphyService> logger)
    : IGiphyService
{
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public async Task<GiphyResponse> GetTrendingAsync(CancellationToken cancellationToken = default)
    {
        const string cacheKey = "giphy:trending";

        var cached = await cacheService.GetAsync<GiphyResponse>(cacheKey, cancellationToken);

        if (cached is not null)
        {
            return cached;
        }

        var sem = _locks.GetOrAdd(cacheKey, _ => new SemaphoreSlim(1, 1));

        await sem.WaitAsync(cancellationToken);

        try
        {
            // Second time in case the previous request cached the response
            // while we were waiting
            var ca = await cacheService.GetAsync<GiphyResponse>(cacheKey, cancellationToken);

            if (ca is not null)
            {
                return ca;
            }

            var response = await giphyClient.GetTrendingAsync(cancellationToken);
            if (response is null)
            {
                throw new EmptyResponseException(cacheKey);
            }

            logger.LogInformation("Storing trending in cache");

            await cacheService.SetAsync(cacheKey, response, cancellationToken);
            return response;
        }
        finally
        {
            sem.Release();
        }
    }

    public async Task<GiphyResponse> SearchAsync(string keyword, CancellationToken cancellationToken = default)
    {
        var kw = keyword.Trim().ToLower();
        ArgumentException.ThrowIfNullOrEmpty(kw);

        var cacheKey = $"giphy:search:{kw}";

        var cached = await cacheService.GetAsync<GiphyResponse>(cacheKey, cancellationToken);

        if (cached is not null)
        {
            return cached;
        }

        var sem = _locks.GetOrAdd(cacheKey, _ => new SemaphoreSlim(1, 1));

        await sem.WaitAsync(cancellationToken);

        try
        {
            var ca = await cacheService.GetAsync<GiphyResponse>(cacheKey, cancellationToken);
            if (ca is not null)
            {
                return ca;
            }

            var response = await giphyClient.SearchAsync(kw, cancellationToken);
            if (response is null)
            {
                throw new EmptyResponseException(cacheKey);
            }

            logger.LogInformation("Storing '{kw}' in cache", kw);
            await cacheService.SetAsync(cacheKey, response, cancellationToken);
            return response;
        }
        finally
        {
            sem.Release();
        }
    }
}