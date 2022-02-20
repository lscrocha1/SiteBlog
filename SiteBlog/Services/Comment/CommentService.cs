namespace SiteBlog.Services.Comment;

using MongoDB.Driver;
using SiteBlog.Adapters;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Helpers;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Repositories.Mongo;

public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> _logger;
    private readonly IMongoRepository<Post> _mongoRepository;
    private readonly IMongoRepository<Comment> _commentRepository;
    private readonly IMongoRepository<Reply> _replyRepository;

    public CommentService(
        ILogger<CommentService> logger,
        IMongoRepository<Post> mongoRepository,
        IMongoRepository<Reply> replyRepository,
        IMongoRepository<Comment> commentRepository)
    {
        _logger = logger;
        _replyRepository = replyRepository;
        _mongoRepository = mongoRepository;
        _commentRepository = commentRepository;
    }

    public async Task AddComment(string postId, AddCommentDto dto, CancellationToken cancellationToken)
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
        catch (NotFoundException)
        {
            _logger.LogError($"Post or comment was not found.");

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while adding a comment. Error {ex.Message}");

            throw;
        }
    }

    public async Task ApproveDeny(CancellationToken cancellationToken, ApproveDenyCommentDto dto)
    {
        try
        {
            _logger.LogInformation($"Getting comment with id {dto.CommentId}");

            if (dto.IsReply)
                await ApproveDenyReply(cancellationToken, dto);
            else
                await ApproveDenyComment(cancellationToken, dto);
        }
        catch (NotFoundException)
        {
            _logger.LogError($"Post or comment was not found.");

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while adding a comment. Error {ex.Message}");

            throw;
        }
    }

    private async Task ApproveDenyComment(CancellationToken cancellationToken, ApproveDenyCommentDto dto)
    {
        var filter = Helper.GetQueryFilter<Comment>(e => e.Id == dto.CommentId);

        var comment = await _commentRepository.GetAsync(filter, cancellationToken);

        if (comment is null)
            throw new NotFoundException();

        _logger.LogInformation($"Comment with id {dto.CommentId} will be approved/denied");

        var update = Builders<Comment>.Update
            .Set(e => e.Approved, dto.Approved);

        await _commentRepository.UpdateAsync(filter, update, cancellationToken);

        _logger.LogInformation($"Comment with id {comment.Id} updated successfully");
    }

    private async Task ApproveDenyReply(CancellationToken cancellationToken, ApproveDenyCommentDto dto)
    {
        var filter = Helper.GetQueryFilter<Reply>(e => e.Id == dto.CommentId);

        var comment = await _replyRepository.GetAsync(filter, cancellationToken);

        if (comment is null)
            throw new NotFoundException();

        _logger.LogInformation($"Comment with id {dto.CommentId} will be approved/denied");

        var update = Builders<Reply>.Update
            .Set(e => e.Approved, dto.Approved);

        await _replyRepository.UpdateAsync(filter, update, cancellationToken);

        _logger.LogInformation($"Comment with id {comment.Id} updated successfully");
    }

    public async Task<CommentToBeApprovedDto[]> GetCommentsToBeApproved(
        CancellationToken cancellationToken,
        int page = 1,
        int limit = 10)
    {
        try
        {
            _logger.LogInformation($"Listing comments to be approved");

            var filter = Helper.GetQueryFilter<Comment>(e => !e.Approved.HasValue);

            var comments = await _commentRepository.GetAsync(filter, cancellationToken, page, limit);

            _logger.LogInformation($"Comments to be approved listed successfully");

            return CommentAdapter.MapCommentToBeApproved(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error happened while listing comments to be approved. Error: {ex.Message}");

            throw;
        }
    }

    public async Task ReplyComment(
        string postId,
        string commentId,
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
        catch (NotFoundException)
        {
            _logger.LogError($"Post or comment was not found.");

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while adding a comment. Error {ex.Message}");

            throw;
        }
    }
}