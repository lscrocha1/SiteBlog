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
    public async Task<ActionResult<List<Tag>>> Get(
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        var tags = await _tagService.Get(search, page, limit);

        return Ok(tags);
    }
}