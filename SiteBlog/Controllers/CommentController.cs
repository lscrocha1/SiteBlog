using Microsoft.AspNetCore.Mvc;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services.Comment;
using System.ComponentModel.DataAnnotations;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/comment")]
[ServiceFilter(typeof(LocalizationAttribute))]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [Route("{postId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddComment(
        [FromRoute][Required] string postId,
        [FromBody] AddCommentDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            await _commentService.AddComment(postId, dto, cancellationToken);

            return StatusCode(201);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("post/{postId}/comment/{commentId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ReplyComment(
        [FromRoute][Required] string postId,
        [FromRoute][Required] string commentId,
        [FromBody] ReplyCommentDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            await _commentService.ReplyComment(postId, commentId, dto, cancellationToken);

            return StatusCode(201);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("comments/approve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(BasicAuthenticationAttribute))]
    public async Task<ActionResult<CommentToBeApprovedDto>> GetCommentsToBeApproved(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        try
        {
            var result = await _commentService.GetCommentsToBeApproved(cancellationToken, page, limit);

            if (result == null || !result.Any())
                return StatusCode(404);

            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("comments/approve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(BasicAuthenticationAttribute))]
    public async Task<ActionResult> ApproveDeny(
        CancellationToken cancellationToken,
        [FromBody] ApproveDenyCommentDto dto)
    {
        try
        {
            await _commentService.ApproveDeny(cancellationToken, dto);

            return Ok();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}