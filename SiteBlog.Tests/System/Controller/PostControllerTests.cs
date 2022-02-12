using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteBlog.Controllers;
using SiteBlog.Domain;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Controller;

public class PostControllerTests
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetPosts());

        var controller = new PostController(mockService.Object);

        // Act
        var result = (OkObjectResult)await controller.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokePostService()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetPosts());

        var controller = new PostController(mockService.Object);

        // Act
        var result = (OkObjectResult)await controller.Get();

        // Assert
        mockService.Verify(
            service => service.GetPosts(null, null, 1, 10),
            Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfPosts()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(PostFixture.GetPosts());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var objectResult = (OkObjectResult)result;

        objectResult.Value.Should().BeOfType<List<Post>>();
    }

    [Fact]
    public async Task Get_OnNoPostsFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IPostService>();

        mockService
            .Setup(service => service.GetPosts(null, null, 1, 10))
            .ReturnsAsync(new List<Post>());

        var controller = new PostController(mockService.Object);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();

        var objectResult = (NotFoundResult)result;

        objectResult.StatusCode.Should().Be(404);
    }
}