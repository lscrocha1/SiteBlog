using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Constants;
using System.Globalization;

namespace SiteBlog.Adapters;

public static class PostAdapter
{
    public static PostDto MapPostDto(Post post)
    {
        var postLanguage = PostContentLanguageEnum.Portuguese;

        var culture = CultureInfo.CurrentCulture;

        if (culture.TwoLetterISOLanguageName.ToLower() != Constants.Language.TwoLetterPortuguese)
            postLanguage = PostContentLanguageEnum.Portuguese;

        var content = post.Contents.Where(e => e.Language == postLanguage).FirstOrDefault()!;

        return new PostDto
        {
            Id = post.Id,
            Title = content.Title,
            Content = content.Body,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            ImageDisplay = post.Display,
            DisplayType = post.DisplayType,
            Description = content.Description,
            Comments = post.Comments.Select(e => new CommentDto
            {
                Content = e.Content,
                CreatedAt = e.CreatedAt,
                UserName = e.UserName,
                Replies = e.Replies.Select(g => new ReplyDto
                { 
                    Id = g.Id,
                    Content = g.Content,
                    UserName = g.UserName,
                    CreatedAt = g.CreatedAt,
                    ReplyingToId = g.ReplyingToId
                })
                .ToList()
            })
            .ToList(),
            Images = post.Images.Select(e => new ImageDto
            {
                Link = e.Link,
                Type = e.Type
            })
            .ToList(),
            Tags = post.Tags.Select(e => new TagDto
            {
                Name = e.Name
            })
            .ToList(),
        };
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