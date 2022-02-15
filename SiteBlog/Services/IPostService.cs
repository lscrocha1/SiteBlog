using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Services;

public interface IPostService
{
    Task<List<PostsDto>> GetPosts(
        string? search = null,
        int? tag = null,
        int page = 1, 
        int limit = 10);

    Task<PostDto?> GetPost(int postId);

    Task CreatePost(CreatePostDto post);
}