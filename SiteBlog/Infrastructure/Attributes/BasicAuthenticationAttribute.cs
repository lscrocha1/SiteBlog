using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace SiteBlog.Infrastructure.Attributes;

public class BasicAuthenticationAttribute : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public BasicAuthenticationAttribute(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var uuid = context.HttpContext.Request.Headers["UUID"];

        if (string.IsNullOrEmpty(uuid))
        {
            ReturnUnauthorizedResult(context);

            return;
        }

        var appSettingUuid = _configuration["BasicUser:Uuid"];

        if (uuid != appSettingUuid)
        {
            ReturnUnauthorizedResult(context);

            return;
        }

        var authHeader = context.HttpContext.Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(authHeader))
        {
            ReturnUnauthorizedResult(context);

            return;
        }

        var authValue = AuthenticationHeaderValue.Parse(authHeader);

        var bytes = Convert.FromBase64String(authValue.Parameter!);

        var credentials = Encoding.UTF8.GetString(bytes).Split(':', 2);

        if (credentials == null || credentials.Length == 0)
        {
            ReturnUnauthorizedResult(context);

            return;
        }

        var userName = credentials[0];

        var password = credentials[1];

        var appSettingsUser = _configuration["BasicUser:Username"];

        var appSettingPassword = _configuration["BasicUser:Password"];

        if (userName == null || password == null)
        {
            ReturnUnauthorizedResult(context);

            return;
        }

        if (userName != appSettingsUser)
        {
            ReturnUnauthorizedResult(context);

            return;
        }

        if (HashPassword(password) != appSettingPassword)
        {
            ReturnUnauthorizedResult(context);

            return;
        }
    }

    private static string HashPassword(string input)
    {
        using var md5 = MD5.Create();

        var inputBytes = Encoding.ASCII.GetBytes(input);

        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();

        for (int i = 0; i < hashBytes.Length; i++)
            sb.Append(hashBytes[i].ToString("X2"));

        return sb.ToString();
    }

    private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedResult();
    }
}