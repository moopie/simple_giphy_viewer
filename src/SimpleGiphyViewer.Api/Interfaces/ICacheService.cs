namespace SimpleGiphyViewer.Api.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey, CancellationToken  ct = default);
    Task SetAsync<T>(string cacheKey, T cacheValue, CancellationToken ct = default);
    Task RemoveAsync(string cacheKey, CancellationToken ct = default);
}