namespace SiteBlog.Dto;

public class TagDto
{
    public TagDto()
    {
        Name = string.Empty;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}