using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Image
{
    public Image()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Link = string.Empty;
        Path = string.Empty;
    }

    public string Id { get; set; }

    public string Link { get; set; }

    public string Path { get; set; }

    public PostDisplayTypeEnum Type { get; set; }
}