using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Post
{
    public Post()
    {
        Display = string.Empty;
        Contents = new List<Content>();
        Images = new List<Image>();
        Tags = new List<Tag>();
        Comments = new List<Comment>();
    }

    public ObjectId Id { get; set; }

    public PostDisplayTypeEnum DisplayType { get; set; }

    public string Display { get; set; }

    public DateTime CreatedAt => DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public List<Content> Contents { get; set; }

    public List<Image> Images { get; set; }

    public List<Comment> Comments { get; set; }

    public List<Tag> Tags { get; set; }
}