﻿using SiteBlog.Domain;
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
                PostId = 1,
                CreatedAt = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                ImageDisplay = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            },
            new ListPostDto
            {
                PostId = 2,
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
                Id = 1,
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = 1,
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
                        Id = 1,
                        Link = Guid.NewGuid().ToString()
                    }
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = 1,
                        Name = Guid.NewGuid().ToString()
                    }
                }
            },
            new Post
            {
                Id = 3,
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = 3,
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
                        Id = 4,
                        Link = Guid.NewGuid().ToString()
                    }
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = 5,
                        Name = Guid.NewGuid().ToString()
                    }
                }
            }
        };
    }
}