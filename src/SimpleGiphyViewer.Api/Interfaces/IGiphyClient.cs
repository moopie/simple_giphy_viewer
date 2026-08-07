using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Interfaces;

public interface IGiphyClient
{
    Task<GiphyResponse?> GetTrendingAsync(CancellationToken cancellationToken = default);
    Task<GiphyResponse?> SearchAsync(string keyword, CancellationToken cancellationToken = default);
}