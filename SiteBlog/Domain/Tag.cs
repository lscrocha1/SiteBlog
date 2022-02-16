using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Tag
{
    public Tag()
    {
        Name = string.Empty;
    }

    public ObjectId Id { get; set; }

    public string Name { get; set; }
}