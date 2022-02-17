using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Tag
{
    public Tag()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = string.Empty;
    }

    public string Id { get; set; }

    public string Name { get; set; }
}