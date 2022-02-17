using MongoDB.Bson;

namespace SiteBlog.Domain;

public class Content
{
    public Content()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Title = string.Empty;
        Body = string.Empty;
        Description = string.Empty;
    }

    public string Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public string Description { get; set; }

    public PostContentLanguageEnum Language { get; set; }
}