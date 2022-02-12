using Microsoft.AspNetCore.Mvc;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/post")]
public class PostController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return null;
    }
}