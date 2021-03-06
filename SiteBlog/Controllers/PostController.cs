using Microsoft.AspNetCore.Mvc;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services.Post;

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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<ActionResult<List<Post>>> GetPosts(
        CancellationToken cancellationToken,
        [FromQuery] string? search = null,
        [FromQuery] string? tag = null,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        try
        {
            var posts = await _postService.GetPosts(cancellationToken, search, tag, page, limit);

            if (posts is null || !posts.Any())
                return NotFound();

            return Ok(posts);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{url}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<ActionResult<Post>> GetPost([FromRoute] string url, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postService.GetPost(url, cancellationToken);

            if (post is null)
                return NotFound();

            return Ok(post);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(BasicAuthenticationAttribute))]
    public async Task<ActionResult> CreatePost([FromForm] CreateEditPostDto post, CancellationToken cancellationToken)
    {
        try
        {
            await _postService.CreatePost(post, cancellationToken);

            return StatusCode(201);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(BasicAuthenticationAttribute))]
    public async Task<ActionResult> Edit([FromForm] CreateEditPostDto post, CancellationToken cancellationToken)
    {
        try
        {
            await _postService.EditPost(post, cancellationToken);

            return StatusCode(200);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(BasicAuthenticationAttribute))]
    public async Task<ActionResult> Remove([FromRoute] string id, CancellationToken cancellationToken)
    {
        try
        {
            await _postService.Remove(id, cancellationToken);

            return StatusCode(200);
        }
        catch (NotFoundException)
        {
            return StatusCode(404);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}