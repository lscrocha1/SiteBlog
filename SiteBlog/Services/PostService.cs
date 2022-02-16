using SiteBlog.Dto;

namespace SiteBlog.Services;

public class PostService : IPostService
{
    public async Task CreatePost(CreatePostDto postDto)
    {
        throw new NotImplementedException();
    }

    public async Task<PostDto?> GetPost(int postId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PostsDto>> GetPosts(
        string? search = null,
        int? tag = null,
        int page = 1,
        int limit = 10)
    {
        throw new NotImplementedException();
    }
}