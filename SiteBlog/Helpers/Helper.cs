using MongoDB.Driver;
using System.Linq.Expressions;

namespace SiteBlog.Helpers;

public static class Helper
{
    public static FilterDefinition<T> GetQueryFilter<T>(Expression<Func<T, bool>> filter)
    {
        return Builders<T>.Filter.Where(filter);
    }

    public static string FormatFilePath(string filePath)
    {
        return string.Join("/", filePath.Replace("\\", "/").Replace("\\\\", "/").Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));
    }
}