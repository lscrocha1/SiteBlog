using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using SiteBlog.Controllers;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Exceptions;
using SiteBlog.Services.Comment;
using SiteBlog.Tests.Fixture;
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

        var cancellationToken = PostFixture.GetCancellationToken();

        // Act
        var result = (StatusCodeResult)await controller.AddComment(string.Empty, new AddCommentDto(), cancellationToken);

        // Assert
        result.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task AddComment_Should_InvokeCommentServiceOneTime()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        var cancellationToken = PostFixture.GetCancellationToken();

        // Act
        var result = await controller.AddComment(string.Empty, new AddCommentDto(), cancellationToken);

        // Assert
        mockService.Verify(service => 
            service.AddComment(It.IsAny<string>(), It.IsAny<AddCommentDto>(), cancellationToken), 
            Times.Once());
    }

    [Fact]
    public async Task AddComment_Should_ReturnStatusCode404()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(e => e.AddComment(It.IsAny<string>(), It.IsAny<AddCommentDto>(), cancellationToken))
            .ThrowsAsync(new NotFoundException());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (NotFoundResult)await controller.AddComment(string.Empty, new AddCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        }, cancellationToken);

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task AddComment_Should_ReturnStatusCode500()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(e => e.AddComment(It.IsAny<string>(), It.IsAny<AddCommentDto>(), cancellationToken))
            .ThrowsAsync(It.IsAny<Exception>());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.AddComment(string.Empty, new AddCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        }, cancellationToken);

        // Assert
        result.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task ReplyComment_Should_ReturnStatusCode201()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        var cancellationToken = PostFixture.GetCancellationToken();

        // Act
        var result = (StatusCodeResult)await controller.ReplyComment(string.Empty, string.Empty, new ReplyCommentDto(), cancellationToken);

        // Assert
        result.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task ReplyComment_Should_InvokeCommentServiceOneTime()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var controller = new CommentController(mockService.Object);

        var cancellationToken = PostFixture.GetCancellationToken();

        // Act
        var result = await controller.ReplyComment(string.Empty, string.Empty, new ReplyCommentDto(), cancellationToken);

        // Assert
        mockService.Verify(service =>
            service.ReplyComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ReplyCommentDto>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task ReplyComment_Should_ReturnStatusCode404()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(e => e.ReplyComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ReplyCommentDto>(), cancellationToken))
            .ThrowsAsync(new NotFoundException());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (NotFoundResult)await controller.ReplyComment(string.Empty, string.Empty, new ReplyCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        }, cancellationToken);

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task ReplyComment_Should_ReturnStatusCode500()
    {
        // Arrange
        var mockService = new Mock<ICommentService>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockService
            .Setup(e => e.ReplyComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ReplyCommentDto>(), cancellationToken))
            .ThrowsAsync(It.IsAny<Exception>());

        var controller = new CommentController(mockService.Object);

        // Act
        var result = (StatusCodeResult)await controller.ReplyComment(string.Empty, string.Empty, new ReplyCommentDto
        {
            Content = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        }, cancellationToken);

        // Assert
        result.StatusCode.Should().Be(500);
    }
}