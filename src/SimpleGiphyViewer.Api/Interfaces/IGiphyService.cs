using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Interfaces;

public interface IGiphyService
{
    Task<GiphyResponse> GetTrendingAsync(CancellationToken cancellationToken = default);
    Task<GiphyResponse> SearchAsync(string keyword, CancellationToken cancellationToken = default);
}