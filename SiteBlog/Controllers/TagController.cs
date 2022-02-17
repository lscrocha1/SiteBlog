using Microsoft.AspNetCore.Mvc;
using SiteBlog.Domain;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Services;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/tag")]
[ServiceFilter(typeof(LocalizationAttribute))]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Tag>>> Get(
        CancellationToken cancellationToken,
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        try
        {
            var tags = await _tagService.Get(cancellationToken, search, page, limit);

            return Ok(tags);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}