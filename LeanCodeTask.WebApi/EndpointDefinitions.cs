using System.Net.Mime;
using System.Security.Cryptography;
using System.Web;
using LeanCodeTask.WebApi.Database.Repositories;
using LeanCodeTask.WebApi.RedditApiClient;
using LeanCodeTask.WebApi.RedditApiClient.Responses;
using LeanCodeTask.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LeanCodeTask.WebApi;

public static class EndpointDefinitions
{
    public static void UseEndpoints(this WebApplication app)
    {
        app.MapGet("/random", GetRandomRedditImage);
        app.MapGet("/history", GetImagesHistory);
    }

    private static async Task<IResult> GetRandomRedditImage(
        [FromServices] IRedditApiClient redditApi,
        [FromServices] IRedditImagesRepository repo,
        [FromServices] IDateTimeProvider dateTimeProvider,
        [FromServices] IOptions<RedditApiSettings> redditApiSettings)
    {
        var apiResponse = await redditApi.GetHotPostsAsync(redditApiSettings.Value.Subreddit);
        var image = GetImageFromApiResponse(apiResponse);
        await CreateImageRetrievalRecordInDatabase(image, repo, dateTimeProvider);
        
        return Results.Ok(new { imageUrl = HttpUtility.HtmlDecode(image.Source.Url) });
    }

    
    private static async Task<IResult> GetImagesHistory([FromServices] IRedditImagesRepository repo)
    {
        var images = await repo.GetAsync();

        var result = images
            .Select(x => new {imageUrl = x.ImageUrl, retrievalDate = x.RetrievalDate})
            .ToList();
        
        return Results.Ok(result);
    }

    
    private static Image GetImageFromApiResponse(HotPostsResponse apiResponse)
    {
        var children = apiResponse.Data.Children.ToList();
        Image? image = null;
        while(image is null)
        {
            image = children[RandomNumberGenerator.GetInt32(0, children.Count)].Data.Preview?.Images?.FirstOrDefault();
        }

        return image;
    }

    
    private static async Task CreateImageRetrievalRecordInDatabase(Image image,
        IRedditImagesRepository repo,
        IDateTimeProvider dateTimeProvider)
    {
        var imageUrl = HttpUtility.HtmlDecode(image.Source.Url);
        await repo.CreateAsync(new()
        {
            ImageUrl = imageUrl,
            RetrievalDate = dateTimeProvider.GetCurrentDateTimeUTC()
        });
    }
}