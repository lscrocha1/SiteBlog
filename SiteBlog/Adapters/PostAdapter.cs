using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Constants;
using SiteBlog.Infrastructure.Extensions;
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
            Tags = post.Tags.Where(e => !string.IsNullOrEmpty(e.Name.Trim())).Select(e => new TagDto
            {
                Name = e.Name
            })
            .ToList(),
        };
    }

    public static Post MapCreatePostDto(CreateEditPostDto postDto)
    {
        var contents = new List<Content>
        {
            new Content
            {
                Body = postDto.PtBody,
                Title = postDto.PtTitle,
                Description = postDto.PtDescription,
                Language = PostContentLanguageEnum.Portuguese
            },
            new Content
            {
                Body = postDto.EnBody,
                Title = postDto.EnTitle,
                Description = postDto.EnDescription,
                Language = PostContentLanguageEnum.English
            }
        };

        return new Post
        {
            Display = postDto.Display,
            DisplayType = postDto.DisplayType,
            Contents = contents,
            EnUrl = CreatePostUrl(postDto.EnTitle),
            PtUrl = CreatePostUrl(postDto.PtTitle),
            Images = postDto.Images
                .Select(e => new Image
                {
                    Link = e.Link,
                    Type = e.Type
                })
                .ToList(),
            Tags = postDto.Tags.Split(";")
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => new Tag
                {
                    Name = e
                })
                .ToList()
        };
    }

    private static string CreatePostUrl(string title)
    {
        title = title
            .Trim()
            .Replace(" ", "-")
            .Replace("!", "")
            .Replace("?", "")
            .Replace("(", "")
            .Replace(")", "");

        return title.RemoveDiacritics().ToLower();
    }
}