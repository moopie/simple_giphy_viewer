using SimpleGiphyViewer.Api.Models;

namespace SimpleGiphyViewer.Api.Interfaces;

public interface IGiphyService
{
    Task<GiphyResponse> GetTrendingAsync();
    Task<GiphyResponse> SearchAsync(string keyword);
}