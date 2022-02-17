using MongoDB.Bson;

namespace SiteBlog.Dto;

public class ReplyDto
{
    public ReplyDto()
    {
        Id = string.Empty;
        UserName = string.Empty;
        Content = string.Empty;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public string? ReplyingToId { get; set; }

    public DateTime CreatedAt { get; set; }
}