using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Reply
{
    public Reply()
    {
        UserName = string.Empty;
        Content = string.Empty;
    }

    public ObjectId Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt => DateTime.Now;

    public ObjectId? ReplyingToId { get; set; }

    public bool Approved { get; set; }
}