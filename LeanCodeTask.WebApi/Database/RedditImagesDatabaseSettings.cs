namespace LeanCodeTask.WebApi.Database;

public class RedditImagesDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ImagesCollectionName { get; set; } = null!;
}