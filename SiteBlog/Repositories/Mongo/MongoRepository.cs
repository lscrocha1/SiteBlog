using MongoDB.Driver;
using SiteBlog.Configuration;

namespace SiteBlog.Repositories.Mongo;

public class MongoRepository<T> : IMongoRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<MongoRepository<T>> _logger;

    public MongoRepository(MongoConfiguration configuration, ILogger<MongoRepository<T>> logger)
    {
        _logger = logger;

        var database = new MongoClient(configuration.ConnectionString).GetDatabase(configuration.DatabaseName);

        _collection = database.GetCollection<T>(typeof(T).Name);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"MongoRepository: - Adding entity {typeof(T).Name}");

            var opts = new InsertOneOptions
            {
                BypassDocumentValidation = false
            };

            await _collection.InsertOneAsync(entity, opts, cancellationToken);

            _logger.LogInformation($"MongoRepository: - Entity {typeof(T).Name} added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MongoRepository - AddAsync - An error happened. Error: {ex.Message}");

            throw;
        }
    }

    public async Task DeleteAsync(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"MongoRepository: - Removing entity {typeof(T).Name}");

            await _collection.DeleteOneAsync(filterDefinition, cancellationToken);

            _logger.LogInformation($"MongoRepository: - Entity {typeof(T).Name} removed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MongoRepository - DeleteAsync - An error happened. Error: {ex.Message}");

            throw;
        }
    }

    public async Task<List<T>> GetAsync(
        FilterDefinition<T> filter,
        CancellationToken cancellationToken,
        int page = 1,
        int limit = 10)
    {
        try
        {
            _logger.LogInformation($"MongoRepository: - Filtering entity {typeof(T).Name}");

            return await _collection
                .Find(filter)
                .Skip((page - 1) * limit)
                .Limit(limit)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MongoRepository - GetAsync - An error happened. Error: {ex.Message}");

            throw;
        }
    }

    public async Task<T?> GetAsync(FilterDefinition<T> filter, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"MongoRepository: - Filtering entity {typeof(T).Name}");

            return await _collection
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MongoRepository - GetAsync - An error happened. Error: {ex.Message}");

            throw;
        }
    }

    public async Task UpdateAsync(
        FilterDefinition<T> filterDefinition, 
        UpdateDefinition<T> updateDefinition,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"MongoRepository: - Updating entity {typeof(T).Name}");

            await _collection.UpdateOneAsync(
                filterDefinition, 
                updateDefinition, 
                cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MongoRepository - UpdateAsync - An error happened. Error: {ex.Message}");

            throw;
        }
    }
}