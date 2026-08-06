using Microsoft.AspNetCore.Mvc;
using SimpleGiphyViewer.Api.Interfaces;

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
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTrendingAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var res = await giphyService.GetTrendingAsync(cancellationToken);
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
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAsync(string keyword, CancellationToken cancellationToken = default)
    {
        try
        {
            var kw = keyword.Trim().ToLower();
            if (string.IsNullOrEmpty(kw))
            {
                return Ok();
            }
            var res = await giphyService.SearchAsync(kw, cancellationToken);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
}