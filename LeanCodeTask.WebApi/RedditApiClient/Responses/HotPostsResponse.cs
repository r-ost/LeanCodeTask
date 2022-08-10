using System.Text.Json.Serialization;

namespace LeanCodeTask.WebApi.RedditApiClient.Responses;

public record HotPostsResponse
{
    [JsonPropertyName("data")]
    public ResponseData Data { get; init; } = null!;
}

public record ResponseData
{
    [JsonPropertyName("children")]
    public IEnumerable<Child> Children { get; init; } = new List<Child>();
}

public record Child
{
    [JsonPropertyName("data")] 
    public PostData Data { get; init; } = null!;
}

public record PostData
{
    [JsonPropertyName("preview")]
    public Preview? Preview { get; init; } = null;
}

public record Preview
{
    [JsonPropertyName("images")]
    public IEnumerable<Image>? Images { get; init; } = new List<Image>();
}

public record Image
{
    [JsonPropertyName("source")]
    public Source Source { get; init; } = null!;
}

public record Source
{
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;
}