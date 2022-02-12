using SiteBlog.Domain;

namespace SiteBlog.Services;

public interface IPostService
{
    Task<List<Post>> GetPosts(
        string? search = null,
        int? tag = null,
        int page = 1, 
        int limit = 10);
}