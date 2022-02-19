namespace SiteBlog.Services.Post;

using MongoDB.Driver;
using SiteBlog.Adapters;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Helpers;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services.File;

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

    public async Task<PostDto?> GetPost(string id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting post with id {id}");

            var filter = Helper.GetQueryFilter<Post>(e => e.Id == id);

            var post = await _mongoRepository.GetAsync(filter, cancellationToken);

            if (post is null)
                return null;

            _logger.LogInformation($"Found post with id {post.Id}");

            return PostAdapter.MapPostDto(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while getting a post. Error: {ex.Message}");

            throw;
        }
    }

    public async Task<List<Post>> GetPosts(
        CancellationToken cancellationToken,
        string? search = null,
        string? tag = null,
        int page = 1,
        int limit = 10)
    {
        try
        {
            var filter = GetFilter(search, tag);

            return await _mongoRepository.GetAsync(filter, cancellationToken, page, limit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happend while getting a list of post. Error: {ex.Message}");

            throw;
        }
    }

    private static FilterDefinition<Post> GetFilter(string? search = null, string? tag = null)
    {
        var filter = Builders<Post>.Filter;

        var filterResult = filter.Empty;

        if (!string.IsNullOrEmpty(search))
        {
            search = search.Trim().ToLower();

            filterResult &= filter.Where(e =>
                e.Contents.Select(g => g.Description).Contains(search)
                || e.Contents.Select(g => g.Title.ToLower()).Contains(search)
                || e.Contents.Select(g => g.Body.ToLower()).Contains(search)
                || e.Tags.Select(g => g.Name.ToLower()).Contains(search));
        }

        if (!string.IsNullOrEmpty(tag))
        {
            tag = tag.Trim().ToLower();

            filterResult &= filter.Where(e => e.Tags.Select(g => g.Name.ToLower()).Contains(tag));
        }

        return filterResult;
    }
}