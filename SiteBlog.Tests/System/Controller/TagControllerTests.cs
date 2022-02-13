﻿using FluentAssertions;
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

        // Act
        var result = await controller.Get();

        // Assert
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_Should_InvokeTagService()
    {
        // Arrange
        var mockService = new Mock<ITagService>();

        var controller = new TagController(mockService.Object);

        // Act
        var result = await controller.Get();

        // Assert
        mockService.Verify(service => service.Get(It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task Get_ReturnsTags()
    {
        // Arrange
        var mockService = new Mock<ITagService>();

        mockService
            .Setup(e => e.Get(It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts().SelectMany(e => e.Tags).ToList());

        var controller = new TagController(mockService.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var objectResult = result.Result as OkObjectResult;

        objectResult!.Value!.Should().NotBeNull();
        objectResult!.Value!.Should().BeOfType<List<Tag>>();
    }
}