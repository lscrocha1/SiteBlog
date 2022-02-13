using Microsoft.AspNetCore.Mvc;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Services;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/post")]
[ServiceFilter(typeof(LocalizationAttribute))]
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
    public async Task<ActionResult<List<ListPostDto>>> GetPosts(
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
    public async Task<ActionResult<Post>> GetPost([FromRoute] int id)
    {
        var post = await _postService.GetPost(id);

        if (post is null)
            return NotFound();

        return Ok(post);
    }
}