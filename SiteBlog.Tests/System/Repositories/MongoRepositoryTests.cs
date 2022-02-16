using FluentAssertions;
using SiteBlog.Domain;
using SiteBlog.Repositories.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Repositories;

public class MongoRepositoryTests
{
    [Fact]
    public async Task GetAsync_Should_ReturnPaginatedPosts()
    {
        // Arrange
        var mongoRepository = new MongoRepository<Post>();

        // Act
        var result = await mongoRepository.GetAsync(page: 1, limit: 5);

        // Assert
        result.Count().Should().Be(5);
    }
}