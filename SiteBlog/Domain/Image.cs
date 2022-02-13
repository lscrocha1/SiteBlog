namespace SiteBlog.Domain;

public class Image
{
    public Image()
    {
        Link = string.Empty;
    }

    public int Id { get; set; }

    public string Link { get; set; }

    public int PostId { get; set; }

    public Post? Post { get; set; }
}