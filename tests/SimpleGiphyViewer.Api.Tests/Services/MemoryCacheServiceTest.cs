using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using SimpleGiphyViewer.Api.Services;
using Xunit;

namespace SimpleGiphyViewer.Api.Tests.Services;

[TestSubject(typeof(MemoryCacheService))]
public class MemoryCacheServiceTest
{
    private readonly MemoryCacheService _cacheService;

    public MemoryCacheServiceTest()
    {
        var memoryCache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = 100
        });
        
        _cacheService = new MemoryCacheService(memoryCache);
    }

    [Fact]
    public async Task GetAsync_Missing()
    {
        var result = await _cacheService.GetAsync<string>("missing", TestContext.Current.CancellationToken);
        
        Assert.Null(result);
    }

    [Fact]
    public async Task SetAsync_Then_GetAsync()
    {
        await _cacheService.SetAsync("abc", "def", TestContext.Current.CancellationToken);
        
        var result = await _cacheService.GetAsync<string>("abc", TestContext.Current.CancellationToken);
        
        Assert.Equal("def", result);
    }
}