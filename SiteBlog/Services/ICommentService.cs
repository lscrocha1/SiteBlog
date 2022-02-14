using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface ICommentService
{
    Task AddComment(int postId, AddCommentDto dto);

    Task ReplyComment(int postId, int commentId, ReplyCommentDto dto);
}