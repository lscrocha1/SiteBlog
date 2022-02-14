using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Context;
using SiteBlog.Infrastructure.Exceptions;
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
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var commentService = new CommentService(mockContext.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await commentService.AddComment(99, new AddCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }));
    }

    [Fact]
    public async Task AddComment_Should_AddComment_IntoPost_ByPostGivenId()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable();

        var postMock = posts.BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(postMock.Object);
        mockContext.Setup(e => e.Comments).Returns(postMock.Object.SelectMany(e => e.Comments).BuildMockDbSet().Object);

        var commentService = new CommentService(mockContext.Object);

        var postId = 1;
        var content = Guid.NewGuid().ToString();
        var userName = Guid.NewGuid().ToString();

        // Act
        await commentService.AddComment(postId, new AddCommentDto
        {
            Content = content,
            UserName = userName
        });

        // Assert 
        posts
            .Where(e => e.Id == postId)
            .SelectMany(e => e.Comments)
            .Should()
            .Contain(e => e.Content == content && e.UserName == userName);
    }

    [Fact]
    public async Task ReplyComment_Should_ThrowException_If_NotFound_PostWithGivenPostId()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var commentService = new CommentService(mockContext.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await commentService.ReplyComment(99, 99, new AddCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }));
    }

    [Fact]
    public async Task ReplyComment_Should_ThrowException_If_NotFound_CommentWithGivenPostId()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);
        mockContext.Setup(e => e.Comments).Returns(posts.Object.SelectMany(e => e.Comments).BuildMockDbSet().Object);
        mockContext.Setup(e => e.Replies).Returns(posts.Object.SelectMany(e => e.Comments).SelectMany(e => e.Replies).BuildMockDbSet().Object);

        var commentService = new CommentService(mockContext.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await commentService.ReplyComment(1, 99, new AddCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }));
    }

    [Fact]
    public async Task ReplyComment_Should_AddComment_IntoPost_ByPostGivenId()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable();

        var postMock = posts.BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(postMock.Object);
        mockContext.Setup(e => e.Comments).Returns(postMock.Object.SelectMany(e => e.Comments).BuildMockDbSet().Object);
        mockContext.Setup(e => e.Replies).Returns(postMock.Object.SelectMany(e => e.Comments).SelectMany(e => e.Replies).BuildMockDbSet().Object);

        var commentService = new CommentService(mockContext.Object);

        var postId = 1;
        var commentId = 1;
        var content = Guid.NewGuid().ToString();
        var userName = Guid.NewGuid().ToString();

        // Act
        await commentService.ReplyComment(postId, commentId, new AddCommentDto
        {
            Content = content,
            UserName = userName
        });

        // Assert 
        posts
            .Where(e => e.Id == postId)
            .SelectMany(e => e.Comments)
            .SelectMany(e => e.Replies)
            .Should()
            .Contain(e => e.Content == content && e.UserName == userName);
    }
}