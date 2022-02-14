using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Constants;
using System.Globalization;

namespace SiteBlog.Adapters;

public static class PostAdapter
{
    public static List<PostsDto> MapPostsDto(List<PostsQueryDto> posts)
    {
        var culture = CultureInfo.CurrentCulture;

        if (culture.TwoLetterISOLanguageName.ToLower() == Constants.Language.TwoLetterPortuguese)
            return MapPortuguesePosts(posts);

        return MapEnglishPosts(posts);
    }

    private static List<PostsDto> MapPortuguesePosts(List<PostsQueryDto> posts)
    {
        return posts.Select(e => new PostsDto
        {
            PostId = e.Id,
            Title = e.PtTitle,
            CreatedAt = e.CreatedAt,
            Description = e.PtDescription,
            ImageDisplay = e.ImageDisplay,
            QuantityComments = e.QuantityComments
        })
        .ToList();
    }

    private static List<PostsDto> MapEnglishPosts(List<PostsQueryDto> posts)
    {
        return posts.Select(e => new PostsDto
        {
            PostId = e.Id,
            Title = e.EnTitle,
            CreatedAt = e.CreatedAt,
            Description = e.EnDescription,
            ImageDisplay = e.ImageDisplay,
            QuantityComments = e.QuantityComments
        })
        .ToList();
    }

    public static PostDto MapPostDto(Post post)
    {
        var culture = CultureInfo.CurrentCulture;

        if (culture.TwoLetterISOLanguageName.ToLower() == Constants.Language.TwoLetterPortuguese)
            return MapPortuguesePost(post);

        return MapEnglishPost(post);
    }

    private static PostDto MapPortuguesePost(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.PtTitle,
            Content = post.PtContent,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            ImageDisplay = post.ImageDisplay,
            Description = post.PtDescription,
            Images = post.Images.Select(e => new ImageDto
            {
                Id = e.Id,
                Link = e.Link,
            })
            .ToList(),
            Tags = post.Tags.Select(e => new TagDto
            {
                Id = e.Id,
                Name = e.Name
            })
            .ToList(),
            Comments = post.Comments.Select(e => new CommentDto
            {
                Id = e.Id,
                Content = e.Content,
                UserName = e.UserName,
                CreatedAt = e.CreatedAt,
                Replies = e.Replies.Select(g => new ReplyDto
                {
                    Id = g.Id,
                    Content = g.Content,
                    UserName = g.UserName,
                    CreatedAt = g.CreatedAt
                })
                .ToList()
            })
            .ToList()
        };
    }

    private static PostDto MapEnglishPost(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.EnTitle,
            Content = post.EnContent,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            ImageDisplay = post.ImageDisplay,
            Description = post.EnDescription,
            Images = post.Images.Select(e => new ImageDto
            {
                Id = e.Id,
                Link = e.Link,
            })
            .ToList(),
            Tags = post.Tags.Select(e => new TagDto
            {
                Id = e.Id,
                Name = e.Name
            })
            .ToList(),
            Comments = post.Comments.Select(e => new CommentDto
            {
                Id = e.Id,
                Content = e.Content,
                UserName = e.UserName,
                CreatedAt = e.CreatedAt,
                Replies = e.Replies.Select(g => new ReplyDto
                {
                    Id = g.Id,
                    Content = g.Content,
                    UserName = g.UserName,
                    CreatedAt = g.CreatedAt
                })
                .ToList()
            })
            .ToList()
        };
    }
}