namespace SiteBlog.Configuration;

public class MongoConfiguration
{
    public MongoConfiguration()
    {
        DatabaseName = string.Empty;
        ConnectionString = string.Empty;
    }

    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }
}