namespace SimpleGiphyViewer.Api.Models;

public sealed record GiphyResponse(
    IReadOnlyList<Gif> Data,
    Pagination Pagination,
    Meta Meta
);

public sealed record Gif(
    string Type,
    string Id,
    string Url,
    string Title,
    GifImages Images
);

public sealed record GifImages(
    GifImage Original
);

public sealed record GifImage(
    string Url,
    string Width,
    string Height
);

public sealed record Pagination(
    int TotalCount,
    int Count,
    int Offset
);

public sealed record Meta(
    int Status,
    string Msg,
    string ResponseId
);