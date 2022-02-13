﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteBlog.Controllers;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System.Collections.Generic;
using System.Linq;
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

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetListPostDtos());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts();

        // Assert
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPosts_OnSuccess_InvokePostService()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetListPostDtos());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts();

        // Assert
        mockService.Verify(
            service => service.GetPosts(null, null, 1, 10),
            Times.Once());
    }

    [Fact]
    public async Task GetPosts_OnSuccess_ReturnsListOfPosts()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetListPostDtos());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts();

        // Assert
        (result.Result as OkObjectResult)!.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPosts_OnNoPostsFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(new List<ListPostDto>());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPosts();

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

        mockService
            .Setup(service => service.GetPost(It.IsAny<PostId>()))
            .ReturnsAsync(PostFixture.GetPosts().FirstOrDefault()!);

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(1);

        // Assert
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPost_OnSuccess_InvokePostService()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPost(It.IsAny<PostId>()))
            .ReturnsAsync(PostFixture.GetPosts().FirstOrDefault()!);

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(1);

        // Assert
        mockService.Verify(service => 
            service.GetPost(It.IsAny<PostId>()), 
            Times.Once);
    }

    [Fact]
    public async Task GetPost_OnSuccess_ReturnsSinglePostResult()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPost(It.IsAny<PostId>()))
            .ReturnsAsync(PostFixture.GetPosts().FirstOrDefault()!);

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(1);

        // Assert
        (result.Result as OkObjectResult)!.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPost_OnNoItemFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPost(It.IsAny<PostId>()))
            .ReturnsAsync((Post)null!);

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.GetPost(99);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();

        var objectResult = result.Result as NotFoundResult;

        objectResult!.StatusCode.Should().Be(404);
    }
}