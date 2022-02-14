using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteBlog.Controllers;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Controller;

public class CommentControllerTests
{
    [Fact]
    public async Task AddComment_Should_ReturnStatusCode201()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.AddComment(1, new AddCommentDto());

        // Assert
        result.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task AddComment_Should_InvokeCommentServiceOneTime()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        // Act
        var result = await controller.AddComment(1, new AddCommentDto());

        // Assert
        mockService.Verify(service => 
            service.AddComment(It.IsAny<int>(), It.IsAny<AddCommentDto>()), 
            Times.Once());
    }

    [Fact]
    public async Task AddComment_Should_ReturnStatusCode404()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        mockService
            .Setup(e => e.AddComment(It.IsAny<int>(), It.IsAny<AddCommentDto>()))
            .ThrowsAsync(new NotFoundException());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (NotFoundResult)await controller.AddComment(1, new AddCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        });

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task AddComment_Should_ReturnStatusCode500()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        mockService
            .Setup(e => e.AddComment(It.IsAny<int>(), It.IsAny<AddCommentDto>()))
            .ThrowsAsync(It.IsAny<Exception>());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.AddComment(1, new AddCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        });

        // Assert
        result.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task ReplyComment_Should_ReturnStatusCode201()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.ReplyComment(1, 1, new AddCommentDto());

        // Assert
        result.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task ReplyComment_Should_InvokeCommentServiceOneTime()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        // Act
        var result = await controller.ReplyComment(1, 1, new AddCommentDto());

        // Assert
        mockService.Verify(service =>
            service.ReplyComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<AddCommentDto>()),
            Times.Once());
    }

    [Fact]
    public async Task ReplyComment_Should_ReturnStatusCode404()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        mockService
            .Setup(e => e.ReplyComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<AddCommentDto>()))
            .ThrowsAsync(new NotFoundException());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (NotFoundResult)await controller.ReplyComment(1, 1, new AddCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        });

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task ReplyComment_Should_ReturnStatusCode500()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        mockService
            .Setup(e => e.ReplyComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<AddCommentDto>()))
            .ThrowsAsync(It.IsAny<Exception>());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.ReplyComment(1, 1, new AddCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        });

        // Assert
        result.StatusCode.Should().Be(500);
    }
}