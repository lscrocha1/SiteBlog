namespace SiteBlog.Dto;

public class PostsQueryDto
{
    public PostsQueryDto()
    {
        EnTitle = string.Empty;
        PtTitle = string.Empty;
        PtDescription = string.Empty;
        EnDescription = string.Empty;
        ImageDisplay = string.Empty;
        EnContent = string.Empty;
        PtContent = string.Empty;
        Tags = new List<TagDto>();
    }

    public int Id { get; set; }

    public string EnTitle { get; set; }

    public string PtTitle { get; set; }

    public string EnDescription { get; set; }

    public string PtDescription { get; set; }

    public string ImageDisplay { get; set; }

    public string EnContent { get; set; }

    public string PtContent { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int QuantityComments { get; set; }

    public List<TagDto> Tags { get; set; }
}