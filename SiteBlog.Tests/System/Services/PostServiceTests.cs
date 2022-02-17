using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System;
using System.Collections.Generic;
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

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository.Setup(e => e.GetAsync(
            It.IsAny<FilterDefinition<Post>>(), 
            cancellationToken, 
            It.IsAny<int>(), 
            It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts());

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(cancellationToken);

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetPosts_ReturnsListOfPostsPaginated()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository.Setup(e => e.GetAsync(
            It.IsAny<FilterDefinition<Post>>(),
            cancellationToken,
            It.IsAny<int>(),
            It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts());

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(cancellationToken, page: 1, limit: 1);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPost_ReturnSinglePost()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository
            .Setup(e => e.GetAsync(It.IsAny<FilterDefinition<Post>>(), cancellationToken))
            .ReturnsAsync(PostFixture.GetPosts().FirstOrDefault());

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPost("post", cancellationToken);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPosts_ReturnsTheCorrectCommentQuantity()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Post>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository.Setup(e => e.GetAsync(
            It.IsAny<FilterDefinition<Post>>(),
            cancellationToken,
            It.IsAny<int>(),
            It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts());

        var postService = new PostService(PostFixture.GetLogger<PostService>(), mockRepository.Object);

        // Act
        var result = await postService.GetPosts(cancellationToken, page: 1, limit: 10);

        // Assert
        var post = result.FirstOrDefault(e => e.Id == new ObjectId());

        post.Should().NotBeNull();

        post!.Comments.Count.Should().Be(1);
    }
}