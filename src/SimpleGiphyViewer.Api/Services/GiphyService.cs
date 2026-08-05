using Microsoft.Extensions.Caching.Memory;
using SimpleGiphyViewer.Api.Exceptions;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Services;

public class GiphyService(
    IGiphyClient giphyClient,
    IMemoryCache memoryCache,
    ILogger<GiphyService> logger
    ) : IGiphyService
{
    public async Task<GiphyResponse> GetTrendingAsync()
    {
        var cacheKey = "giphy:trending";

        if (!memoryCache.TryGetValue<GiphyResponse>(cacheKey, out var res))
        {
            logger.LogInformation("Got trending from cache");
            
            if (res is null)
            {
                throw new EmptyResponseException(cacheKey);
            }
            
            return res;
        }

        var response = await giphyClient.GetTrendingAsync();

        if (response is null)
        {
            throw new EmptyResponseException(cacheKey);
        }
        
        logger.LogInformation("Storing trending in cache");
        memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(5));

        return response;
    }
    
    public async Task<GiphyResponse> SearchAsync(string keyword)
    {
        var cacheKey = $"giphy:search:{keyword}";

        if (!memoryCache.TryGetValue<GiphyResponse>(cacheKey, out var res))
        {
            logger.LogInformation("Got '{Keyword}' from cache", keyword);
            
            if (res is null)
            {
                throw new EmptyResponseException(cacheKey);
            }
            
            return res;
        }

        var response = await giphyClient.SearchAsync(keyword);

        if (response is null)
        {
            throw new EmptyResponseException(cacheKey);
        }
        
        logger.LogInformation("Storing '{Keyword}' in cache", keyword);
        memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(5));

        return response;
    }
}