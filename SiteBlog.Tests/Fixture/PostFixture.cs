using Microsoft.Extensions.Logging;
using Moq;
using SiteBlog.Domain;
using SiteBlog.Dto;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SiteBlog.Tests.Fixture;

public static class PostFixture
{
    public static ILogger<T> GetLogger<T>()
    {
        var mockLogger = new Mock<ILogger<T>>();

        return mockLogger.Object;
    }

    public static CancellationToken GetCancellationToken()
    {
        var cancellationTokenSource = new CancellationTokenSource();

        return cancellationTokenSource.Token;
    }

    public static List<PostsDto> GetListPostDtos()
    {
        return new List<PostsDto>
        {
            new PostsDto
            {
                PostId = 1,
                CreatedAt = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            },
            new PostsDto
            {
                PostId = 2,
                CreatedAt = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
            }
        };
    }

    public static PostDto GetPostDto()
    {
        return new PostDto
        {
            Id = 1,
            Comments = new List<CommentDto>
            {
                new CommentDto
                {
                    Content = Guid.NewGuid().ToString(),
                    UserName = Guid.NewGuid().ToString()
                }
            },
            Description = Guid.NewGuid().ToString(),
            Content = Guid.NewGuid().ToString(),
            ImageDisplay = Guid.NewGuid().ToString(),
            Title = Guid.NewGuid().ToString(),
            Images = new List<ImageDto>
            {
                new ImageDto
                {
                    Link = Guid.NewGuid().ToString()
                }
            },
            Tags = new List<TagDto>
            {
                new TagDto
                {
                    Name = Guid.NewGuid().ToString()
                }
            }
        };
    }

    public static List<Tag> GetTags()
    {
        return null!;
    }

    public static List<Post> GetPosts()
    {
        return null!;
    }
}