using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Constants;
using System.Globalization;

namespace SiteBlog.Adapters;

public static class PostAdapter
{
    public static List<PostsDto> MapPostsDto(List<Post> posts)
    {
        return null!;
    }

    public static PostDto MapPostDto(Post post)
    {
        return null!;
    }

    public static Post MapCreatePostDto(CreatePostDto postDto)
    {
        return new Post
        {
            Display = postDto.Display,
            DisplayType = postDto.DisplayType,
            Contents = postDto.Contents
                .Select(e => new Content
                {
                    Body = e.Body,
                    Description = e.Description,
                    Language = e.Language,
                    Title = e.Title
                })
                .ToList(),
            Images = postDto.Images
                .Select(e => new Image
                {
                    Link = e.Link,
                    Type = e.Type
                })
                .ToList(),
            Tags = postDto.Tags
                .Select(e => new Tag
                {
                    Name = e.Name
                })
                .ToList()
        };
    }
}