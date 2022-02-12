namespace SiteBlog.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int)]
public partial struct PostId { }

public class Post
{
    public Post()
    {
        Title = string.Empty;
        Description = string.Empty;
        ImageDisplay = string.Empty;
        EnContent = string.Empty;
        PtContent = string.Empty;
        Tags = new List<Tag>();
        Images = new List<Image>();
        Comments = new List<Comment>();
    }

    public PostId Id { get; set; } = new PostId();

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageDisplay { get; set; }

    public string EnContent { get; set; }

    public string PtContent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public IList<Tag> Tags { get; set; }

    public IList<Image> Images { get; set; }

    public IList<Comment> Comments { get; set; }
}