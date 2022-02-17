using MongoDB.Bson;
using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface ICommentService
{
    Task AddComment(string postId, AddCommentDto dto, CancellationToken cancellationToken);

    Task ReplyComment(string postId, string commentId, ReplyCommentDto dto, CancellationToken cancellationToken);
}