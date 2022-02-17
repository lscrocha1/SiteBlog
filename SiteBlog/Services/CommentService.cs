using MongoDB.Bson;
using MongoDB.Driver;
using SiteBlog.Adapters;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Helpers;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Repositories.Mongo;

namespace SiteBlog.Services;

public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> _logger;
    private readonly IMongoRepository<Post> _mongoRepository;

    public CommentService(ILogger<CommentService> logger, IMongoRepository<Post> mongoRepository)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
    }

    public async Task AddComment(ObjectId postId, AddCommentDto dto, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Adding a new comment");

            var filter = Helper.GetQueryFilter<Post>(e => e.Id == postId);

            var post = await _mongoRepository.GetAsync(filter, cancellationToken);

            if (post is null)
                throw new NotFoundException();

            if (post.Comments is null)
                post.Comments = new List<Comment>();

            post.Comments.Add(CommentAdapter.MapComment(dto));

            var update = Builders<Post>.Update
                .Set(e => e.Comments, post.Comments);

            await _mongoRepository.UpdateAsync(filter, update, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while adding a comment. Error {ex.Message}");

            throw;
        }
    }

    public async Task ReplyComment(
        ObjectId postId, 
        ObjectId commentId, 
        ReplyCommentDto dto, 
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Adding a new comment");

            var filter = Helper.GetQueryFilter<Post>(e => e.Id == postId);

            var post = await _mongoRepository.GetAsync(filter, cancellationToken);

            if (post is null || post.Comments is null)
                throw new NotFoundException();

            var comment = post.Comments.FirstOrDefault(e => e.Id == commentId);

            if (comment is null)
                throw new NotFoundException();

            if (comment.Replies is null)
                comment.Replies = new List<Reply>();

            comment.Replies.Add(CommentAdapter.MapReply(dto));

            var update = Builders<Post>.Update
                .Set(e => e.Comments, post.Comments);

            await _mongoRepository.UpdateAsync(filter, update, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while adding a comment. Error {ex.Message}");

            throw;
        }
    }
}