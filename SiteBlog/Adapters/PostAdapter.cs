using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Adapters;

public static class PostAdapter
{
    public static List<ListPostDto> MapListPostDto(List<Post> posts)
    {
        return posts.Select(e => new ListPostDto
        {
            PostId = e.Id,
            Title = e.Title,
            CreatedAt = e.CreatedAt,
            Description = e.Description,
            ImageDisplay = e.ImageDisplay,
        })
        .ToList();
    }
}