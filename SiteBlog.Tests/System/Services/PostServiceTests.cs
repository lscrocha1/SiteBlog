using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Infrastructure.Context;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Services;

public class PostServiceTests
{
    [Fact]
    public async Task GetPosts_ReturnsListOfPosts()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var mockSet = new Mock<DbSet<Post>>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetPosts_ReturnsListOfPostsPaginated()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var mockSet = new Mock<DbSet<Post>>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts(1, 1);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }
}