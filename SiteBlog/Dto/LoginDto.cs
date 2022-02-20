using System.ComponentModel.DataAnnotations;

namespace SiteBlog.Dto;

public class LoginDto
{
    public LoginDto()
    {
        Username = string.Empty;
        Password = string.Empty;
    }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}