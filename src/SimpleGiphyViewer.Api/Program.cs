using SimpleGiphyViewer.Api.Clients;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Options;
using SimpleGiphyViewer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Ideally redis/valkey will replace this but for a demo it's good enough (i hope?)
builder.Services.AddMemoryCache();

builder.Services.AddTransient<IGiphyService, GiphyService>();

builder.Services.AddHttpClient<IGiphyClient, GiphyClient>();

builder.Services.Configure<GiphyOptions>(
    builder.Configuration.GetSection(GiphyOptions.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
