using SiteBlog.Domain;

namespace SiteBlog.Dto;

public class ImageDto
{
    public ImageDto()
    {
        Link = string.Empty;
    }

    public string Link { get; set; }

    public PostDisplayTypeEnum Type { get; set; }
}