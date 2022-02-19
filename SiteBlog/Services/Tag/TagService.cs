namespace SiteBlog.Services.Tag;

using MongoDB.Driver;
using SiteBlog.Repositories.Mongo;

public class TagService : ITagService
{
    private readonly ILogger<TagService> _logger;
    private readonly IMongoRepository<Domain.Tag> _mongoRepository;

    public TagService(ILogger<TagService> logger, IMongoRepository<Domain.Tag> mongoRepository)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
    }

    public async Task<List<Domain.Tag>> Get(
        CancellationToken cancellationToken,
        string? search = null,
        int page = 1,
        int limit = 10)
    {

        try
        {
            _logger.LogInformation($"Listing tags with search {search}");

            var filter = Builders<Domain.Tag>.Filter;

            var filterResult = filter.Empty;

            if (!string.IsNullOrEmpty(search))
                filterResult = filter.Where(e => e.Name.ToLower().Contains(search.ToLower()));

            return await _mongoRepository.GetAsync(filterResult, cancellationToken, page, limit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An erro happened while listing tags. Error: {ex.Message}");

            throw;
        }
    }
}