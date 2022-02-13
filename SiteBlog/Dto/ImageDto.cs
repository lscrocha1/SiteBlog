namespace SiteBlog.Dto;

public class ImageDto
{
    public ImageDto()
    {
        Link = string.Empty;
    }

    public int Id { get; set; }

    public string Link { get; set; }
}