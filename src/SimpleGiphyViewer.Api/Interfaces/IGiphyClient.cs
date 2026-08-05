using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Interfaces;

public interface IGiphyClient
{
    Task<GiphyResponse?> GetTrendingAsync();
    Task<GiphyResponse?> SearchAsync(string keyword);
}