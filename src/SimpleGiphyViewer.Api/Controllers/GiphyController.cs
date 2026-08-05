using Microsoft.AspNetCore.Mvc;
using SimpleGiphyViewer.Api.Interfaces;
using SimpleGiphyViewer.Api.Services;

namespace SimpleGiphyViewer.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class GiphyController(
    IGiphyService giphyService,
    ILogger<GiphyController> logger
    ) : ControllerBase
{
    [HttpGet]
    [Route("trending")]
    public async Task<IActionResult> GetTrendingAsync()
    {
        try
        {
            var res = await giphyService.GetTrendingAsync();
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("search/{keyword}")]
    public async Task<IActionResult> SearchAsync(string keyword)
    {
        try
        {
            var res = await giphyService.SearchAsync(keyword);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
}