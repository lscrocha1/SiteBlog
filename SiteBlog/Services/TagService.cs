using SiteBlog.Domain;

namespace SiteBlog.Services;

public class TagService : ITagService
{
    public async Task<List<Tag>> Get(
        string? search = null,
        int page = 1,
        int limit = 10)
    {
        throw new NotImplementedException();
    }
}