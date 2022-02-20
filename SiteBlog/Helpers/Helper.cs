using MongoDB.Driver;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

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

    public static string HashPassword(string input)
    {
        using var md5 = MD5.Create();

        var inputBytes = Encoding.ASCII.GetBytes(input);

        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();

        for (int i = 0; i < hashBytes.Length; i++)
            sb.Append(hashBytes[i].ToString("X2"));

        return sb.ToString();
    }
}