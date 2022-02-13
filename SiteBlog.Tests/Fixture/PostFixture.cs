using SiteBlog.Domain;
using SiteBlog.Dto;
using System;
using System.Collections.Generic;

namespace SiteBlog.Tests.Fixture;

public static class PostFixture
{
    public static List<ListPostDto> GetListPostDtos()
    {
        return new List<ListPostDto>
        {
            new ListPostDto
            {
                PostId = new PostId(1),
                CreatedAt = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            },
            new ListPostDto
            {
                PostId = new PostId(2),
                CreatedAt = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            }
        };
    }

    public static List<Post> GetPosts()
    {
        return new List<Post>
        {
            new Post
            {
                Id = new PostId(1),
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = new CommentId(1),
                        Content = Guid.NewGuid().ToString(),
                        UserName = Guid.NewGuid().ToString()
                    }
                },
                Description = Guid.NewGuid().ToString(),
                EnContent = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                PtContent = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                Images = new List<Image>
                {
                    new Image
                    {
                        Id = new ImageId(1),
                        Link = Guid.NewGuid().ToString()
                    }
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = new TagId(1),
                        Name = Guid.NewGuid().ToString()
                    }
                }
            },
            new Post
            {
                Id = new PostId(3),
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = new CommentId(3),
                        Content = Guid.NewGuid().ToString(),
                        UserName = Guid.NewGuid().ToString()
                    }
                },
                Description = Guid.NewGuid().ToString(),
                EnContent = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                PtContent = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                Images = new List<Image>
                {
                    new Image
                    {
                        Id = new ImageId(4),
                        Link = Guid.NewGuid().ToString()
                    }
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = new TagId(5),
                        Name = Guid.NewGuid().ToString()
                    }
                }
            }
        };
    }
}