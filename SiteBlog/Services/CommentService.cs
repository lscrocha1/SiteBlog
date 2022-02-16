using SiteBlog.Dto;

namespace SiteBlog.Services;

public class CommentService : ICommentService
{

    public async Task AddComment(int postId, AddCommentDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task ReplyComment(int postId, int commentId, ReplyCommentDto dto)
    {
        throw new NotImplementedException();
    }
}