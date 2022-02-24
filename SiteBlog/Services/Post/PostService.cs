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
    private readonly IFileService _fileService;
    private readonly ILogger<PostService> _logger;
    private readonly IMongoRepository<Post> _mongoRepository;

    public PostService(IFileService fileService, ILogger<PostService> logger, IMongoRepository<Post> mongoRepository)
    {
        _fileService = fileService;
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

            if (postDto.File != null)
            {
                post.Display = await _fileService.SaveFile(postDto.File);
            }

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

    public async Task<Post?> GetPost(string url, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting post with url {url}");

            var filter = Helper.GetQueryFilter<Post>(e => e.PtUrl == url || e.EnUrl == url);

            return await _mongoRepository.GetAsync(filter, cancellationToken);
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

            var posts = await _mongoRepository.GetAsync(filter, cancellationToken, page, limit);

            foreach (var post in posts)
            {
                post.Comments = post.Comments.Where(e => e.Approved.HasValue && e.Approved.Value).ToList();

                foreach (var comment in post.Comments)
                {
                    comment.Replies = comment.Replies.Where(e => e.Approved.HasValue && e.Approved.Value).ToList();
                }
            }

            return posts;
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