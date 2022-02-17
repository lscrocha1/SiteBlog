using SiteBlog.Domain;

namespace SiteBlog.Dto;

public class ContentDto
{
    public ContentDto()
    {
        Title = string.Empty;
        Body = string.Empty;
        Description = string.Empty;
    }

    public string Title { get; set; }

    public string Body { get; set; }

    public string Description { get; set; }

    public PostContentLanguageEnum Language { get; set; }
}