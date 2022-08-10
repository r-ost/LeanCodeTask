using LeanCodeTask.WebApi.Database.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LeanCodeTask.WebApi.Database.Repositories;

public interface IRedditImagesRepository
{
    Task<List<RedditImage>> GetAsync();
    Task<RedditImage?> GetAsync(string id);
    Task CreateAsync(RedditImage newImage);
    Task UpdateAsync(string id, RedditImage updatedImage);
    Task RemoveAsync(string id);
}

public class RedditImagesRepository : IRedditImagesRepository
{
    private readonly IMongoCollection<RedditImage> _imagesCollection;
    
    public RedditImagesRepository(IOptions<RedditImagesDatabaseSettings> redditImagesDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            redditImagesDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            redditImagesDatabaseSettings.Value.DatabaseName);

        _imagesCollection = mongoDatabase.GetCollection<RedditImage>(
            redditImagesDatabaseSettings.Value.ImagesCollectionName);
    }
    
    public async Task<List<RedditImage>> GetAsync() =>
        await _imagesCollection.Find(_ => true).ToListAsync();

    public async Task<RedditImage?> GetAsync(string id) =>
        await _imagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(RedditImage newImage) =>
        await _imagesCollection.InsertOneAsync(newImage);

    public async Task UpdateAsync(string id, RedditImage updatedImage) =>
        await _imagesCollection.ReplaceOneAsync(x => x.Id == id, updatedImage);

    public async Task RemoveAsync(string id) =>
        await _imagesCollection.DeleteOneAsync(x => x.Id == id);

}