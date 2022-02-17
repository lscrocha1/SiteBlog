using MongoDB.Bson;
using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface ICommentService
{
    Task AddComment(ObjectId postId, AddCommentDto dto, CancellationToken cancellationToken);

    Task ReplyComment(ObjectId postId, ObjectId commentId, ReplyCommentDto dto, CancellationToken cancellationToken);
}