using MongoDB.Driver;
using System.Linq.Expressions;

namespace SiteBlog.Helpers;

public static class Helper
{
    public static FilterDefinition<T> GetQueryFilter<T>(Expression<Func<T, bool>> filter)
    {
        return Builders<T>.Filter.Where(filter);
    }
}