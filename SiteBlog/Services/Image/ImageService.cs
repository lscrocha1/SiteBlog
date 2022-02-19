namespace SiteBlog.Services.Image;

using SiteBlog.Domain;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services.File;
using System;
using System.Threading.Tasks;

public class ImageService : IImageService
{
    private readonly IFileService _fileService;
    private readonly ILogger<ImageService> _logger;
    private readonly IMongoRepository<Image> _mongoRepository;

    public ImageService(IFileService fileService, ILogger<ImageService> logger, IMongoRepository<Image> mongoRepository)
    {
        _fileService = fileService;
        _logger = logger;
        _mongoRepository = mongoRepository;
    }

    public async Task<Stream> GetImage(string path)
    {
        try
        {
            _logger.LogInformation($"Getting image with path {path}");

            return await _fileService.GetImage(path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happened while getting a image. Error: {ex.Message}");

            throw;
        }
    }

    public async Task<string> SaveImage(IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Saving a new image");

            var path = await _fileService.SaveFile(file);

            _logger.LogInformation($"Saved a image with path {path}");

            var image = new Image
            {
                Path = path,
                Type = PostDisplayTypeEnum.Image
            };

            await _mongoRepository.AddAsync(image, cancellationToken);

            _logger.LogInformation($"Image {path} saved in mongo repository");

            return path;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error happened while saving image. Error: {ex.Message}");

            throw;
        }
    }
}