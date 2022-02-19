using FluentAssertions;
using MongoDB.Driver;
using Moq;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services.Tag;
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
        var mockRepository = new Mock<IMongoRepository<Domain.Tag>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository.Setup(e => e.GetAsync(
            It.IsAny<FilterDefinition<Domain.Tag>>(),
            cancellationToken,
            It.IsAny<int>(),
            It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts().SelectMany(e => e.Tags).ToList());

        var tagService = new TagService(PostFixture.GetLogger<TagService>(), mockRepository.Object);

        // Act
        var tags = await tagService.Get(cancellationToken);

        // Assert
        tags.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Get_ReturnsTagsPaginated()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Domain.Tag>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository.Setup(e => e.GetAsync(
            It.IsAny<FilterDefinition<Domain.Tag>>(),
            cancellationToken,
            It.IsAny<int>(),
            It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts().SelectMany(e => e.Tags).ToList());

        var tagService = new TagService(PostFixture.GetLogger<TagService>(), mockRepository.Object);

        // Act
        var tags = await tagService.Get(cancellationToken, page: 1, limit: 1);

        // Assert
        tags.Should().NotBeNullOrEmpty();
        tags.Should().HaveCount(1);
    }

    [Fact]
    public async Task Get_ReturnsTagsFilteredBySearch()
    {
        // Arrange
        var mockRepository = new Mock<IMongoRepository<Domain.Tag>>();

        var cancellationToken = PostFixture.GetCancellationToken();

        mockRepository.Setup(e => e.GetAsync(
            It.IsAny<FilterDefinition<Domain.Tag>>(),
            cancellationToken,
            It.IsAny<int>(),
            It.IsAny<int>()))
            .ReturnsAsync(PostFixture.GetPosts().SelectMany(e => e.Tags).ToList());

        var tagService = new TagService(PostFixture.GetLogger<TagService>(), mockRepository.Object);

        // Act
        var tags = await tagService.Get(cancellationToken, search: Guid.NewGuid().ToString());

        // Assert
        tags.Should().NotBeNullOrEmpty();
        tags.Should().HaveCount(1);
    }
}