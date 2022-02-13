using Microsoft.EntityFrameworkCore;
using SiteBlog.Adapters;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Context;

namespace SiteBlog.Services;

public class PostService : IPostService
{
    private readonly BlogContext _blogContext;

    public PostService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public async Task<PostDto?> GetPost(int postId)
    {
        var result = await _blogContext
            .Posts!
            .Where(e => e.Id == postId)
            .FirstOrDefaultAsync();

        if (result is null)
            return null;

        return PostAdapter.MapPostDto(result);
    }

    public async Task<List<PostsDto>> GetPosts(
        string? search = null,
        int? tag = null,
        int page = 1,
        int limit = 10)
    {
        var query = _blogContext
            .Posts!
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(e =>
                e.EnTitle.ToLower().Contains(search.ToLower())
                || e.PtTitle.ToLower().Contains(search.ToLower())
                || e.EnDescription.ToLower().Contains(search.ToLower())
                || e.PtDescription.ToLower().Contains(search.ToLower())
                || e.EnContent.ToLower().Contains(search.ToLower())
                || e.PtContent.ToLower().Contains(search.ToLower()));
        }

        if (tag.HasValue)
        {
            query = query.Where(e => e.Tags.Select(e => e.Id).Contains(tag.Value));
        }

        var result = await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return PostAdapter.MapPostsDto(result);
    }
}