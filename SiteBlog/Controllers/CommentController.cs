using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services;
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
        [FromRoute][Required] ObjectId postId,
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
        [FromRoute][Required] ObjectId postId,
        [FromRoute][Required] ObjectId commentId,
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
}