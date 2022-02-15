using System.ComponentModel.DataAnnotations;

namespace SiteBlog.Dto;

public class CreatePostTagDto
{
    public CreatePostTagDto()
    {
        Name = string.Empty;
    }

    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }
}