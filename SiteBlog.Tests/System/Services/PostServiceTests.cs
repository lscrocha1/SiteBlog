using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Infrastructure.Context;
using SiteBlog.Services;
using SiteBlog.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SiteBlog.Tests.System.Services;

public class PostServiceTests
{
    [Fact]
    public async Task GetPosts_ReturnsListOfPosts()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var tagsMock = PostFixture.GetTags().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetPosts_ReturnsListOfPostsPaginated()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var tagsMock = PostFixture.GetTags().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts(page: 1, limit: 1);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPosts_ReturnsFilteredResult()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts();

        posts.Add(new Post
        {
            EnTitle = "Experience"
        });

        var mockSet = posts.AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(mockSet.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts(search: "Experience", null, page: 1, limit: 10);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPosts_ReturnsFilteredByTagResult()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts();

        var tagId = 888;

        posts.Add(new Post
        {
            Tags = new List<PostTag>
            {
                new PostTag
                {
                    TagId = tagId
                }
            }
        });

        var mockSet = posts.AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(mockSet.Object);

        var tagsMock = PostFixture.GetTags().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts(tag: tagId, page: 1, limit: 10);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetPost_ReturnSinglePost()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPost(1);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPost_ReturnNull_IfNotFound()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var posts = PostFixture.GetPosts().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(posts.Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPost(99);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetPosts_ReturnsTheCorrectCommentQuantity()
    {
        // Arrange
        var mockContext = new Mock<BlogContext>();

        var tagsMock = PostFixture.GetTags().AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Tags).Returns(tagsMock.Object);

        var posts = PostFixture.GetPosts();

        var postId = 9;

        posts.Add(new Post
        {
            Id = postId,   
            Comments = new List<Comment>
            {
                new Comment(),
                new Comment()
            }
        });

        var mockSet = posts.AsQueryable().BuildMockDbSet();

        mockContext.Setup(e => e.Posts).Returns(mockSet.Object);
        mockContext.Setup(e => e.Comments).Returns(mockSet.Object.SelectMany(e => e.Comments).BuildMockDbSet().Object);

        var postService = new PostService(mockContext.Object);

        // Act
        var result = await postService.GetPosts(page: 1, limit: 10);

        // Assert
        var post = result.FirstOrDefault(e => e.PostId == postId);

        post.Should().NotBeNull();

        post!.QuantityComments.Should().Be(2);
    }
}