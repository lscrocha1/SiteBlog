namespace SiteBlog.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int)]
public partial struct ImageId { }

public class Image
{
    public Image()
    {
        Link = string.Empty;
    }

    public ImageId Id { get; set; } = new ImageId();

    public string Link { get; set; }

    public PostId PostId { get; set; }

    public Post? Post { get; set; }
}