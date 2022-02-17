using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface IPostService
{
    Task<List<Post>> GetPosts(
        CancellationToken cancellationToken,
        string? search = null,
        string? tag = null,
        int page = 1, 
        int limit = 10);

    Task<PostDto?> GetPost(string postTitle, CancellationToken cancellationToken);

    Task CreatePost(CreatePostDto post, CancellationToken cancellationToken);
}