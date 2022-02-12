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
        int page = 1,
        int limit = 10)
    {
        return await _blogContext
            .Posts!
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}