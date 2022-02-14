namespace SiteBlog.Dto;

public class ReplyDto
{
    public ReplyDto()
    {
        UserName = string.Empty;
        Content = string.Empty;
    }

    public int Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}