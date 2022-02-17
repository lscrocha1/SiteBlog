using SiteBlog.Domain;

namespace SiteBlog.Services;

public interface ITagService
{
    Task<List<Tag>> Get(
        CancellationToken cancellationToken,
        string? search = null,
        int page = 1,
        int limit = 10);
}