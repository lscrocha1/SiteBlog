namespace SiteBlog.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int)]
public partial struct TagId { }

public class Tag
{
    public Tag()
    {
        Name = string.Empty;
    }

    public TagId Id { get; set; } = new TagId();

    public string Name { get; set; }
}