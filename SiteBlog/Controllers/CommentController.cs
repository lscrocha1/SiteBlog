using Microsoft.AspNetCore.Mvc;
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
        [FromRoute][Required] int postId,
        [FromBody] AddCommentDto dto)
    {
        try
        {
            await _commentService.AddComment(postId, dto);

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