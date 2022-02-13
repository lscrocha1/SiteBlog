using System.ComponentModel.DataAnnotations;

namespace SiteBlog.Dto;

public class AddCommentDto
{
    public AddCommentDto()
    {
        UserName = string.Empty;
        Content = string.Empty;
    }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }
}