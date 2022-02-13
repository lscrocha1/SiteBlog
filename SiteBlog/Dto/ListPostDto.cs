using SiteBlog.Domain;

namespace SiteBlog.Dto;

public class ListPostDto
{
    public ListPostDto()
    {
        Title = string.Empty;
        Description = string.Empty;
        ImageDisplay = string.Empty;
    }

    public int PostId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageDisplay { get; set; }

    public DateTime CreatedAt { get; set; }
}