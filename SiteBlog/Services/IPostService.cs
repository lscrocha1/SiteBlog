using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface IPostService
{
    Task<List<ListPostDto>> GetPosts(
        string? search = null,
        int? tag = null,
        int page = 1, 
        int limit = 10);

    Task<Post?> GetPost(PostId postId);
}