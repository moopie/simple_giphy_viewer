using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Models;
using SimpleGiphyViewer.Api.Options;

namespace SimpleGiphyViewer.Api.Clients;

public class GiphyClient(HttpClient httpClient, IOptions<GiphyOptions> options, ILogger<GiphyClient> logger)
    : IGiphyClient
{
    private readonly GiphyOptions _options = options.Value;

    public async Task<GiphyResponse?> GetTrendingAsync(CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(_options.ApiKey);

        var query = QueryHelpers.AddQueryString(
            "v1/gifs/trending",
            new Dictionary<string, string?>
            {
                { "api_key", _options.ApiKey },
            });
        return await httpClient.GetFromJsonAsync<GiphyResponse>(query, cancellationToken);
    }

    public async Task<GiphyResponse?> SearchAsync(string keyword, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(keyword);
        ArgumentException.ThrowIfNullOrWhiteSpace(_options.ApiKey);

        var query = QueryHelpers.AddQueryString(
            "v1/gifs/search",
            new Dictionary<string, string?>
            {
                { "q", keyword },
                { "api_key", _options.ApiKey },
            });
        return await httpClient.GetFromJsonAsync<GiphyResponse>(query, cancellationToken);
    }
}