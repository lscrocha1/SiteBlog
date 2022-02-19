using System.ComponentModel.DataAnnotations;

namespace SiteBlog.Dto;

public class SaveImageDto
{
    [Required]
    public IFormFile File { get; set; }
}