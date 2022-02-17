using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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

    public static List<Post> GetPosts()
    {
        return new List<Post>
        {
            new Post
            {
                Id = new ObjectId(),
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Approved = true,
                        Id = new ObjectId(),
                        Content = string.Empty,
                        UserName = string.Empty,
                        Replies = new List<Reply>
                        {
                            new Reply
                            {
                                Approved = true,
                                Id = new ObjectId(),
                                Content = string.Empty,
                                UserName = string.Empty,
                            }
                        }
                    }
                },
                Contents = new List<Content>
                {
                    new Content
                    {
                        Body = string.Empty,
                        Description = string.Empty,
                        Id = new ObjectId(),
                        Language = PostContentLanguageEnum.English,
                        Title = "post"
                    },
                    new Content
                    {
                        Body = string.Empty,
                        Description = string.Empty,
                        Id = new ObjectId(),
                        Language = PostContentLanguageEnum.Portuguese,
                        Title = string.Empty
                    }
                },
                Display = string.Empty,
                DisplayType = PostDisplayTypeEnum.Image,
                UpdatedAt = DateTime.Now,
                Images = new List<Image>
                {
                    new Image
                    {
                        Id = new ObjectId(),
                        Link = string.Empty,
                        Type = PostDisplayTypeEnum.Image
                    }
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = new ObjectId(),
                        Name = string.Empty
                    }
                }
            }
        };
    }

    public static PostDto GetPostDto()
    {
        return new PostDto
        {
            Id = new ObjectId(),
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
}