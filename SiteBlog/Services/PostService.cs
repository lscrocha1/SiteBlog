using Microsoft.EntityFrameworkCore;
using SiteBlog.Domain;
using SiteBlog.Infrastructure.Context;

namespace SiteBlog.Services;

public class PostService : IPostService
{
    private readonly BlogContext _blogContext;

    public PostService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public async Task<List<Post>> GetPosts(
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
                e.Title.ToLower().Contains(search.ToLower())
                || e.Description.ToLower().Contains(search.ToLower())
                || e.EnContent.ToLower().Contains(search.ToLower())
                || e.PtContent.ToLower().Contains(search.ToLower()));
        }

        if (tag.HasValue)
        {
            query = query.Where(e => e.Tags.Select(e => e.Id).Contains(new TagId(tag.Value)));
        }

        return await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}