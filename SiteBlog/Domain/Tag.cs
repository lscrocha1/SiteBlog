namespace SiteBlog.Domain;

public class Tag
{
    public Tag()
    {
        Name = string.Empty;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}