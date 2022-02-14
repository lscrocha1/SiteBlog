namespace SiteBlog.Dto;

public class CommentDto
{
    public CommentDto()
    {
        UserName = string.Empty;
        Content = string.Empty;
        Replies = new List<ReplyDto>();
    }

    public int Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<ReplyDto> Replies { get; set; }
}