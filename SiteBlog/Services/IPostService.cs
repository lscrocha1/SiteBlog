using SiteBlog.Domain;

namespace SiteBlog.Services;

public interface IPostService
{
    Task<List<Post>> GetPosts();
}