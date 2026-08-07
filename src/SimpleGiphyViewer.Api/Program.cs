using System.Text.Json;
using Microsoft.Extensions.Options;
using SimpleGiphyViewer.Api.Clients;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Options;
using SimpleGiphyViewer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: replace with redis/valkey
// for when there are more than 1 service consuming said cache
builder.Services.AddMemoryCache(options =>
{
   options.SizeLimit = 1_000; // Don't want the cache to hog all the memory
});

builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

builder.Services.AddTransient<IGiphyService, GiphyService>();

builder.Services.AddHttpClient<IGiphyClient, GiphyClient>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<GiphyOptions>>().Value;
    
    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    DictionaryKeyPolicy =  JsonNamingPolicy.SnakeCaseLower,
});

builder.Services.Configure<GiphyOptions>(
    builder.Configuration.GetSection(GiphyOptions.SectionName));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
