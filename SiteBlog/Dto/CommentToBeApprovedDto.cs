namespace SiteBlog.Dto;

public class CommentToBeApprovedDto
{
    public CommentToBeApprovedDto()
    {
        Id = string.Empty;
        Username = string.Empty;
        Content = string.Empty;
    }

    public string Id { get; set; }

    public string Username { get; set; }

    public string Content { get; set; }

    public bool IsReply { get; set; }
}