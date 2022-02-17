using MongoDB.Bson;

namespace SiteBlog.Dto;

public class PostsDto
{
    public PostsDto()
    {
        Title = string.Empty;
        Description = string.Empty;
        ImageDisplay = string.Empty;
        Tags = new List<TagDto>();
    }

    public ObjectId PostId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageDisplay { get; set; }

    public DateTime CreatedAt { get; set; }

    public int QuantityComments { get; set; }

    public List<TagDto> Tags { get; set; }
}