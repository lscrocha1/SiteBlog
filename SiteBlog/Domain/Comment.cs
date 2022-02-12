namespace SiteBlog.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int)]
public partial struct CommentId { }

public class Comment
{
    public Comment()
    {
        UserName = string.Empty;
        Content = string.Empty;
    }

    public CommentId Id { get; set; } = new CommentId();

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public PostId PostId { get; set; }

    public Post? Post { get; set; }
}