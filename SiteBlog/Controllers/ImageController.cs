using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services.Image;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/image")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ServiceFilter(typeof(BasicAuthenticationAttribute))]
    public async Task<ActionResult<string>> Save([FromForm] SaveImageDto image, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _imageService.SaveImage(image.File, cancellationToken);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(string path)
    {
        try
        {
            var stream = await _imageService.GetImage(path);

            if (stream == null)
                return StatusCode(404);

            var name = path?.Split('/', '\\')?.Last();

            new FileExtensionContentTypeProvider().TryGetContentType(path, out var mimeType);

            if (string.IsNullOrWhiteSpace(mimeType))
                mimeType = "application/octet-stream";

            return File(stream, mimeType, name);
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