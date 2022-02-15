using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Infrastructure.Context;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Services;

public class TagServiceTests
{
    [Fact]
    public async Task Get_ReturnsTags()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable();

        var postMock = posts.BuildMockDbSet();

        var tagsMock = PostFixture.GetTags().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(postMock.Object);
        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        var tagService = new TagService(mockContext.Object);

        // Act
        var tags = await tagService.Get();

        // Assert
        tags.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Get_ReturnsTagsPaginated()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable();

        var postMock = posts.BuildMockDbSet();

        var tagsMock = PostFixture.GetTags().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        mockContext.Setup(e => e.Posts).Returns(postMock.Object);

        var tagService = new TagService(mockContext.Object);

        // Act
        var tags = await tagService.Get(page: 1, limit: 1);

        // Assert
        tags.Should().NotBeNullOrEmpty();
        tags.Should().HaveCount(1);
    }

    [Fact]
    public async Task Get_ReturnsTagsFilteredBySearch()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable();

        var tagName = Guid.NewGuid().ToString();

        var postMock = posts.BuildMockDbSet();

        var tagList = PostFixture.GetTags();

        tagList.Add(new Tag
        {
            Name = tagName
        });

        var tagsMock = tagList.AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(postMock.Object);
        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        var tagService = new TagService(mockContext.Object);

        // Act
        var tags = await tagService.Get(search: tagName);

        // Assert
        tags.Should().NotBeNullOrEmpty();
        tags.Should().HaveCount(1);
    }
}