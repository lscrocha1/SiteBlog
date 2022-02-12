using Microsoft.AspNetCore.Mvc;
using SiteBlog.Services;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/post")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet(Name = "GetPosts")]
    public async Task<IActionResult> Get()
    {
        var posts = await _postService.GetPosts();

        if (posts is null || !posts.Any())
            return NotFound();

        return Ok(posts);
    }
}