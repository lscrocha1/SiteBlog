using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Comment
{
    public Comment()
    {
        Id = ObjectId.GenerateNewId().ToString();
        UserName = string.Empty;
        Content = string.Empty;
        Replies = new List<Reply>();
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt => DateTime.Now;

    public List<Reply> Replies { get; set; }

    public bool Approved { get; set; }
}