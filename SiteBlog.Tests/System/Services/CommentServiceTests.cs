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
            await commentService.AddComment(new PostId(99), new AddCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            }));
    }
}