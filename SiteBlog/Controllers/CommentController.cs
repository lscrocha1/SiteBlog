using Microsoft.AspNetCore.Mvc;
using SiteBlog.Dto;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/comment")]
public class CommentController : ControllerBase
{
    [HttpPost]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddComment(
        [FromRoute] int postId,
        [FromBody] AddCommentDto dto)
    {
        return Ok("all good");
    }
}