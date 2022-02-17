namespace SiteBlog.Dto;

public class CommentDto
{
    public CommentDto()
    {
        UserName = string.Empty;
        Content = string.Empty;
        Replies = new List<ReplyDto>();
    }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<ReplyDto> Replies { get; set; }
}