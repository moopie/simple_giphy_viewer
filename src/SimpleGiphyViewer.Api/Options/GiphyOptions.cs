namespace SimpleGiphyViewer.Api.Options;

public class GiphyOptions
{
    public const string SectionName = "Giphy";

    public string ApiKey { get; init; } = string.Empty;
    public string BaseUrl { get; init; } = "https://api.giphy.com/v1/";
}
