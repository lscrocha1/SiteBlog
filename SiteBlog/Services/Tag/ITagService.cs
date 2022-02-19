namespace SiteBlog.Services.Tag;

using SiteBlog.Domain;

public interface ITagService
{
    Task<List<Tag>> Get(
        CancellationToken cancellationToken,
        string? search = null,
        int page = 1,
        int limit = 10);
}