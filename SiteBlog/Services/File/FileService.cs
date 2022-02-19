namespace SiteBlog.Services.File;

using SiteBlog.Helpers;
using SiteBlog.Infrastructure.Exceptions;
using System.IO;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<Stream> GetImage(string path)
    {
        try
        {
            path = Helper.FormatFilePath(path);

            var folderPath = _configuration["FileRootPath"];

            var fullFilePath = Path.Combine(folderPath, path);

            var exists = File.Exists(fullFilePath);

            if (!exists)
                return null!;

            return Task.FromResult<Stream>(File.OpenRead(fullFilePath));
        }
        catch
        {
            return null!;
        }
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        var folderPath = _configuration["FileRootPath"];

        await using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        var bytes = memoryStream.ToArray();

        var basePath = $"Images/{Guid.NewGuid()}/{file.FileName}";

        var physicalPath = Path.Combine(folderPath, basePath);

        var fullFolder = Path.GetDirectoryName(physicalPath);

        if (!Directory.Exists(fullFolder))
            Directory.CreateDirectory(fullFolder);

        using var image = new FileStream(physicalPath, FileMode.Create);

        await image.WriteAsync(bytes.AsMemory(0, bytes.Length));

        await image.FlushAsync();

        return basePath;
    }
}