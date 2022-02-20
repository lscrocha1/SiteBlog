using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Reply
{
    public Reply()
    {
        Id = ObjectId.GenerateNewId().ToString();
        UserName = string.Empty;
        Content = string.Empty;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt => DateTime.Now;

    public string? ReplyingToId { get; set; }

    public bool? Approved { get; set; }
}