using MongoDB.Bson;

namespace SiteBlog.Dto;

public class ReplyDto
{
    public ReplyDto()
    {
        UserName = string.Empty;
        Content = string.Empty;
    }

    public ObjectId Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public ObjectId? ReplyingToId { get; set; }

    public DateTime CreatedAt { get; set; }
}