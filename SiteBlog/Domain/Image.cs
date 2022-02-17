using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Image
{
    public Image()
    {
        Link = string.Empty;
    }

    public ObjectId Id { get; set; }

    public string Link { get; set; }

    public PostDisplayTypeEnum Type { get; set; }
}