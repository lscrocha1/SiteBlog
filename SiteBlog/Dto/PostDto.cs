using MongoDB.Bson;
using SiteBlog.Domain;

namespace SiteBlog.Dto;

public class PostDto
{
    public PostDto()
    {
        Title = string.Empty;
        Description = string.Empty;
        ImageDisplay = string.Empty;
        Content = string.Empty;
        Tags = new List<TagDto>();
        Images = new List<ImageDto>();
        Comments = new List<CommentDto>();
    }

    public ObjectId Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageDisplay { get; set; }

    public PostDisplayTypeEnum DisplayType { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public IList<TagDto> Tags { get; set; }

    public IList<ImageDto> Images { get; set; }

    public IList<CommentDto> Comments { get; set; }
}