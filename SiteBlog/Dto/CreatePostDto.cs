using System.ComponentModel.DataAnnotations;

namespace SiteBlog.Dto;

public class CreatePostDto
{
    public CreatePostDto()
    {
        EnTitle = string.Empty;
        PtTitle = string.Empty;
        PtDescription = string.Empty;
        EnDescription = string.Empty;
        ImageDisplay = string.Empty;
        EnContent = string.Empty;
        PtContent = string.Empty;
        Tags = new List<CreatePostTagDto>();
    }

    [Required]
    public string EnTitle { get; set; }

    [Required]
    public string PtTitle { get; set; }

    [Required]
    public string EnDescription { get; set; }

    [Required]
    public string PtDescription { get; set; }

    [Required]
    public string ImageDisplay { get; set; }

    [Required]
    public string EnContent { get; set; }

    [Required]
    public string PtContent { get; set; }

    [Required]
    public List<CreatePostTagDto> Tags { get; set; }
}