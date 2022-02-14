namespace SiteBlog.Domain;

public class Reply
{
    public Reply()
    {
        UserName = string.Empty;
        Content = string.Empty;
    }

    public int Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int CommentId { get; set; }

    public Comment? Comment { get; set; }
}