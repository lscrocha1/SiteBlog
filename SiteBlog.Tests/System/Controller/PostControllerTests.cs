using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteBlog.Controllers;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Services.Post;
using SiteBlog.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Controller;

public class PostControllerTests
{
    [Fact]
    public async Task GetPosts_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPosts(cancellationToken, null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetPosts());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts(cancellationToken);

        // Assert
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPosts_OnSuccess_InvokePostService()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPosts(cancellationToken, null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetPosts());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts(cancellationToken);

        // Assert
        mockService.Verify(
            service => service.GetPosts(cancellationToken, null, null, 1, 10),
            Times.Once());
    }

    [Fact]
    public async Task GetPosts_OnSuccess_ReturnsListOfPosts()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPosts(cancellationToken, null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetPosts());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts(cancellationToken);

        // Assert
        (result.Result as OkObjectResult)!.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPosts_OnNoPostsFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPosts(cancellationToken, null, null, 1, 10))
            .ReturnsAsync(new List<Post>());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts(cancellationToken);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();

        var objectResult = result.Result as NotFoundResult;

        objectResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetPost_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPost(It.IsAny<string>(), cancellationToken))
            .ReturnsAsync(PostFixture.GetPostDto());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(Guid.NewGuid().ToString(), cancellationToken);

        // Assert
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPost_OnSuccess_InvokePostService()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPost(It.IsAny<string>(), cancellationToken))
            .ReturnsAsync(PostFixture.GetPostDto());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(Guid.NewGuid().ToString(), cancellationToken);

        // Assert
        mockService.Verify(service =>
            service.GetPost(It.IsAny<string>(), cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task GetPost_OnSuccess_ReturnsSinglePostResult()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPost(It.IsAny<string>(), cancellationToken))
            .ReturnsAsync(PostFixture.GetPostDto());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(Guid.NewGuid().ToString(), cancellationToken);

        // Assert
        (result.Result as OkObjectResult)!.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPost_OnNoItemFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(service => service.GetPost(It.IsAny<string>(), cancellationToken))
            .ReturnsAsync((PostDto)null!);

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(Guid.NewGuid().ToString(), cancellationToken);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();

        var objectResult = result.Result as NotFoundResult;

        objectResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreatePost_Should_ReturnStatusCode201()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var controller = new PostController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.CreatePost(null!, PostFixture.GetCancellationToken());

        // Assert
        result.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task CreatePost_Should_InvokeCreatePost()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        var controller = new PostController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.CreatePost(null!, PostFixture.GetCancellationToken()!);

        // Assert
        mockService.Verify(service =>
            service.CreatePost(It.IsAny<CreatePostDto>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}