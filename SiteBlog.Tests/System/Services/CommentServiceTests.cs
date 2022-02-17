using FluentAssertions;
using MongoDB.Bson;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Services;

public class CommentServiceTests
{
    [Fact]
    public async Task AddComment_Should_ThrowException_If_NotFound_PostWithGivenPostId()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        var commentService = new CommentService(PostFixture.GetLogger<CommentService>(), mockRepository.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await commentService.AddComment(new ObjectId(), new AddCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }, cancellationToken));
    }

    [Fact]
    public async Task ReplyComment_Should_ThrowException_If_NotFound_PostWithGivenPostId()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        var commentService = new CommentService(PostFixture.GetLogger<CommentService>(), mockRepository.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await commentService.ReplyComment(new ObjectId(), new ObjectId(), new ReplyCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }, cancellationToken));
    }

    [Fact]
    public async Task ReplyComment_Should_ThrowException_If_NotFound_CommentWithGivenPostId()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        var commentService = new CommentService(PostFixture.GetLogger<CommentService>(), mockRepository.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await commentService.ReplyComment(new ObjectId(), new ObjectId(), new ReplyCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }, cancellationToken));
    }
}