using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeanCodeTask.WebApi.Models;

public class RedditImage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string ImageUrl { get; set; } = null!;

    public DateTime RetrievalDate { get; set; }
}