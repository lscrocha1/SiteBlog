using SiteBlog.Adapters;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Repositories.Mongo;

namespace SiteBlog.Services;

public class PostService : IPostService
{
    private readonly ILogger<PostService> _logger;
    private readonly IMongoRepository<Post> _mongoRepository;

    public PostService(ILogger<PostService> logger, IMongoRepository<Post> mongoRepository)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
    }

    public async Task CreatePost(
        CreatePostDto postDto,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Adding a new post");

            var post = PostAdapter.MapCreatePostDto(postDto);

            _logger.LogInformation("Post mapped successfully");

            await _mongoRepository.AddAsync(post, cancellationToken);

            _logger.LogInformation("Post added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while adding a post. Error: {ex.Message}");

            throw;
        }
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