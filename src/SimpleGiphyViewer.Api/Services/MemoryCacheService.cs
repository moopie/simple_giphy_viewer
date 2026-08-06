using Microsoft.Extensions.Caching.Memory;
using SimpleGiphyViewer.Api.Interfaces;

namespace SimpleGiphyViewer.Api.Services;

public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly MemoryCacheEntryOptions _options = new()
    {
        // TODO: Consider putting expiration time in config
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        Size = 1
    };
    
    public Task<T?> GetAsync<T>(string cacheKey, CancellationToken  ct = default)
    {
        memoryCache.TryGetValue<T>(cacheKey, out var value);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string cacheKey, T cacheValue, CancellationToken ct = default)
    {
        memoryCache.Set(cacheKey, cacheValue, _options);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string cacheKey, CancellationToken ct = default)
    {
        memoryCache.Remove(cacheKey);
        return Task.CompletedTask;
    }
}