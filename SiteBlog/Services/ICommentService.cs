using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface ICommentService
{
    Task AddComment(PostId postId, AddCommentDto dto);
}