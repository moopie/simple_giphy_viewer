using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Models;
using SimpleGiphyViewer.Api.Options;

namespace SimpleGiphyViewer.Api.Clients;

public partial class GiphyClient(
    HttpClient httpClient,
    IOptions<GiphyOptions> options,
    ILogger<GiphyClient> logger
    ) : IGiphyClient
{
    public async Task<GiphyResponse?> GetTrendingAsync()
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        return await httpClient.GetFromJsonAsync<GiphyResponse>("trending");
    }

    public async Task<GiphyResponse?> SearchAsync(string keyword)
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        return await httpClient.GetFromJsonAsync<GiphyResponse>(keyword);
    }
}