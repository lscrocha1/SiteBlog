namespace SiteBlog.Services.Image;

public interface IImageService
{
    Task<string> SaveImage(IFormFile file, CancellationToken cancellationToken);

    Task<Stream> GetImage(string path);
}