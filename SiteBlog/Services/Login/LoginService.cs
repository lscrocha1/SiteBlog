using SiteBlog.Helpers;
using SiteBlog.Infrastructure.Exceptions;
using System.Text;

namespace SiteBlog.Services.Login;

public class LoginService : ILoginService
{
    private readonly ILogger<LoginService> _logger;
    private readonly IConfiguration _configuration;

    public LoginService(ILogger<LoginService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string Login(string username, string password)
    {
        try
        {
            var userNameConfig = _configuration["BasicUser:Username"];

            var passwordConfig = _configuration["BasicUser:Password"];

            if (userNameConfig != username)
                throw new UnauthorizedException();

            if (passwordConfig != Helper.HashPassword(password))
                throw new UnauthorizedException();

            var basic = $"{username}:{password}";

            var base64 = Encoding.UTF8.GetBytes(basic);

            return Convert.ToBase64String(base64);
        }
        catch (UnauthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Login: An error happened. Ex {ex.Message}");

            throw;
        }
    }
}