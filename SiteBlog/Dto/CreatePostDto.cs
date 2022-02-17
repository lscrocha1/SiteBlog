using SiteBlog.Domain;

namespace SiteBlog.Dto;

public class CreatePostDto
{
    public CreatePostDto()
    {
        Display = string.Empty;
        Contents = new List<ContentDto>();
        Images = new List<ImageDto>();
        Tags = new List<TagDto>();
    }

    public PostDisplayTypeEnum DisplayType { get; set; }

    public string Display { get; set; }

    public List<ContentDto> Contents { get; set; }

    public List<ImageDto> Images { get; set; }

    public List<TagDto> Tags { get; set; }
}