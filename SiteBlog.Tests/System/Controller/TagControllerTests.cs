using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteBlog.Controllers;
using SiteBlog.Domain;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Controller;

public class TagControllerTests
{
    [Fact]
    public async Task Get_Should_ReturnStatusCode200()
    {
        // Arrange
        var mockService = new Mock<ITagService>();

        var controller = new TagController(mockService.Object);

        var cancellationToken = PostFixture.GetCancellationToken();

        // Act
        var result = await controller.Get(cancellationToken);

        // Assert
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_Should_InvokeTagService()
    {
        // Arrange
        var mockService = new Mock<ITagService>();

        var controller = new TagController(mockService.Object);

        var cancellationToken = PostFixture.GetCancellationToken();

        // Act
        var result = await controller.Get(cancellationToken);

        // Assert
        mockService.Verify(service => service.Get(cancellationToken, It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}