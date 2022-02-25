namespace SiteBlog.Services.File;

public interface IFileService
{
    Task<string> SaveFile(IFormFile file);

    Task<Stream> GetImage(string path);

    Task Remove(string path);
}