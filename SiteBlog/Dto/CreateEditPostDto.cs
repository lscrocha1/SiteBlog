using SiteBlog.Domain;

namespace SiteBlog.Dto;

public class CreateEditPostDto
{
    public CreateEditPostDto()
    {
        Id = string.Empty;
        Display = string.Empty;
        PtTitle = string.Empty;
        PtDescription = string.Empty;
        PtBody = string.Empty;
        EnTitle = string.Empty;
        EnDescription = string.Empty;
        EnBody = string.Empty;
        Images = new List<ImageDto>();
        Tags = string.Empty;
    }

    public string Id { get; set; }

    public PostDisplayTypeEnum DisplayType { get; set; }

    public string Display { get; set; }

    public string PtTitle { get; set; }

    public string PtBody { get; set; }

    public string PtDescription { get; set; }

    public IFormFile? File { get; set; }

    public string EnTitle { get; set; }

    public string EnBody { get; set; }

    public string EnDescription { get; set; }

    public List<ImageDto> Images { get; set; }

    public string Tags { get; set; }
}