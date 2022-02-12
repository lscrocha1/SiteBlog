using SiteBlog.Domain;

namespace SiteBlog.Services;

public interface IPostService
{
    Task<List<Post>> GetPosts(
        int page = 1, 
        int limit = 10);
}