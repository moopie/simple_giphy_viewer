using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Interfaces;

public interface IGiphyClient
{
    Task<GiphyResponse?> GetTrendingAsync(CancellationToken cancellationToken);
    Task<GiphyResponse?> SearchAsync(string keyword, CancellationToken cancellationToken);
}