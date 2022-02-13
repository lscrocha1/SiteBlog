namespace SiteBlog.Domain;

public class Post
{
    public Post()
    {
        EnTitle = string.Empty;
        PtTitle = string.Empty;
        PtDescription = string.Empty;
        EnDescription = string.Empty;
        ImageDisplay = string.Empty;
        EnContent = string.Empty;
        PtContent = string.Empty;
        Tags = new List<Tag>();
        Images = new List<Image>();
        Comments = new List<Comment>();
    }

    public int Id { get; set; }

    public string EnTitle { get; set; }

    public string PtTitle { get; set; }

    public string EnDescription { get; set; }

    public string PtDescription { get; set; }

    public string ImageDisplay { get; set; }

    public string EnContent { get; set; }

    public string PtContent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public IList<Tag> Tags { get; set; }

    public IList<Image> Images { get; set; }

    public IList<Comment> Comments { get; set; }
}