using FluentAssertions;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Repositories.Mongo;
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
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetPosts_ReturnsListOfPostsPaginated()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(page: 1, limit: 1);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPosts_ReturnsFilteredResult()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(search: "Experience", null, page: 1, limit: 10);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPosts_ReturnsFilteredByTagResult()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(tag: 1, page: 1, limit: 10);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPost_ReturnSinglePost()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPost(1);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPost_ReturnNull_IfNotFound()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPost(99);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetPosts_ReturnsTheCorrectCommentQuantity()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(page: 1, limit: 10);

        // Assert
        var post = result.FirstOrDefault(e => e.PostId == 1);

        post.Should().NotBeNull();

        post!.QuantityComments.Should().Be(2);
    }
}