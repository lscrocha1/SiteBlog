using Microsoft.AspNetCore.Mvc;
using SiteBlog.Services;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPosts(
        [FromQuery] string? search = null,
        [FromQuery] int? tag = null,
        [FromQuery] int page = 1, 
        [FromQuery] int limit = 10)
    {
        var posts = await _postService.GetPosts(search, tag, page, limit);

        if (posts is null || !posts.Any())
            return NotFound();

        return Ok(posts);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPost([FromRoute]int id)
    {
        var posts = await _postService.GetPosts();

        if (posts is null || !posts.Any())
            return NotFound();

        return Ok(posts);
    }
}