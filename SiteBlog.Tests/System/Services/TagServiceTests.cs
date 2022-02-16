using FluentAssertions;
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
        var posts = PostFixture.GetPosts().AsQueryable();

        var tagService = new TagService();

        // Act
        var tags = await tagService.Get();

        // Assert
        tags.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Get_ReturnsTagsPaginated()
    {
        // Arrange
        var posts = PostFixture.GetPosts().AsQueryable();

        var tagService = new TagService();

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
        var tagService = new TagService();

        // Act
        var tags = await tagService.Get(search: Guid.NewGuid().ToString());

        // Assert
        tags.Should().NotBeNullOrEmpty();
        tags.Should().HaveCount(1);
    }
}