using System.Security.Cryptography;
using System.Web;
using LeanCodeTask.WebApi.RedditApiClient;
using LeanCodeTask.WebApi.RedditApiClient.Responses;
using LeanCodeTask.WebApi.Repositories;
using LeanCodeTask.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;

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
        [FromServices] IDateTimeProvider dateTimeProvider)
    {
        var apiResponse = await redditApi.GetHotPostsAsync();
        var children = apiResponse.Data.Children.ToList();
        Image? image = null;
        while(image is null)
        {
            image = children[RandomNumberGenerator.GetInt32(0, children.Count)].Data.Preview?.Images?.FirstOrDefault();
        }

        var imageUrl = HttpUtility.HtmlDecode(image!.Source.Url);
        await repo.CreateAsync(new()
        {
            ImageUrl = imageUrl,
            RetrievalDate = dateTimeProvider.GetCurrentDateTimeUTC()
        });
    
        return Results.Ok(new
        {
            imageUrl
        });
    }

    private static async Task<IResult> GetImagesHistory([FromServices] IRedditImagesRepository repo)
    {
        var images = await repo.GetAsync();

        var result = images
            .Select(x => new {imageUrl = x.ImageUrl, retrievalDate = x.RetrievalDate})
            .ToList();
        
        return Results.Ok(result);
    }
    
}