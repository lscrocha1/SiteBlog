using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Constants;
using System.Globalization;

namespace SiteBlog.Adapters;

public static class PostAdapter
{
    public static List<ListPostDto> MapListPostDto(List<Post> posts)
    {
        var culture = CultureInfo.CurrentCulture;

        if (culture.TwoLetterISOLanguageName.ToLower() == Constants.Language.TwoLetterPortuguese)
            return MapPortuguesePosts(posts);

        return MapEnglishPosts(posts);
    }

    private static List<ListPostDto> MapEnglishPosts(List<Post> posts)
    {
        return posts.Select(e => new ListPostDto
        {
            PostId = e.Id,
            Title = e.EnTitle,
            CreatedAt = e.CreatedAt,
            Description = e.EnDescription,
            ImageDisplay = e.ImageDisplay,
        })
        .ToList();
    }

    private static List<ListPostDto> MapPortuguesePosts(List<Post> posts)
    {
        return posts.Select(e => new ListPostDto
        {
            PostId = e.Id,
            Title = e.PtTitle,
            CreatedAt = e.CreatedAt,
            Description = e.PtDescription,
            ImageDisplay = e.ImageDisplay,
        })
        .ToList();
    }
}