using MongoDB.Driver;

namespace SiteBlog.Repositories.Mongo;

public interface IMongoRepository<T> where T : class
{
    Task<List<T>> GetAsync(
        FilterDefinition<T> filter,
        CancellationToken cancellationToken,
        int page = 1,
        int limit = 10);

    Task<T?> GetAsync(FilterDefinition<T> filter, CancellationToken cancellationToken);

    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAsync(
        FilterDefinition<T> filterDefinition,
        UpdateDefinition<T> updateDefinition, 
        CancellationToken cancellationToken);

    Task DeleteAsync(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken);
}