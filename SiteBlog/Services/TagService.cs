using Microsoft.EntityFrameworkCore;
using SiteBlog.Domain;
using SiteBlog.Infrastructure.Context;

namespace SiteBlog.Services;

public class TagService : ITagService
{
    private readonly BlogContext _blogContext;

    public TagService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public async Task<List<Tag>> Get(
        string? search = null,
        int page = 1,
        int limit = 10)
    {
        var query = _blogContext
            .Tags!
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => e.Name.ToLower().Contains(search.ToLower()));
        }

        return await query.Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}