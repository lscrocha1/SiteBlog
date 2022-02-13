using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SiteBlog.Controllers;
using SiteBlog.Dto;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Controller;

public class CommentControllerTests
{
    [Fact]
    public async Task AddComment_Should_ReturnStatusCode200()
    {
        // Arrange

        var controller = new CommentController();

        // Act
        var result = (OkObjectResult)await controller.AddComment(1, new AddCommentDto());

        // Assert
        result.StatusCode.Should().Be(200);
    }
}