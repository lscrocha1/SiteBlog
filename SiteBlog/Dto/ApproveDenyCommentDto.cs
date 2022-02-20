namespace SiteBlog.Dto;

public class ApproveDenyCommentDto
{
    public ApproveDenyCommentDto()
    {
        CommentId = string.Empty;
    }

    public string CommentId { get; set; }

    public bool Approved { get; set; }

    public bool IsReply { get; set; }
}