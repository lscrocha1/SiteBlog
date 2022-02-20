using Microsoft.AspNetCore.Mvc;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services.Login;

namespace SiteBlog.Controllers;

[ApiController]
[Route("/v1/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<string> Login([FromBody] LoginDto dto)
    {
        try
        {
            var result = _loginService.Login(dto.Username, dto.Password);

            return Ok(result);
        }
        catch (UnauthorizedException)
        {
            return StatusCode(401);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}