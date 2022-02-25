namespace SiteBlog.Services.Post;

using SiteBlog.Domain;
using SiteBlog.Dto;

public interface IPostService
{
    Task<List<Post>> GetPosts(
        CancellationToken cancellationToken,
        string? search = null,
        string? tag = null,
        int page = 1,
        int limit = 10);

    Task<Post?> GetPost(string url, CancellationToken cancellationToken);

    Task CreatePost(CreateEditPostDto post, CancellationToken cancellationToken);

    Task EditPost(CreateEditPostDto post, CancellationToken cancellationToken);

    Task Remove(string id, CancellationToken cancellationToken);
}