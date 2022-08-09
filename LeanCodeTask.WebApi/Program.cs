using System.Collections.Immutable;
using System.Net.Http.Headers;
using System.Text.Json;
using IdentityModel.Client;
using LeanCodeTask.WebApi.RedditApi;
using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAccessTokenManagement(options =>
{
    options.Client.Clients.Add("reddit-api", new ClientCredentialsTokenRequest
    {
        RequestUri = new Uri(builder.Configuration["RedditApi:TokenEndpoint"]),
        ClientId = builder.Configuration["RedditApi:ClientId"],
        ClientSecret = builder.Configuration["RedditApi:ClientSecret"],
        GrantType = "client_credentials",
    });
});

builder.Services.AddRefitClient<IRedditApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["RedditApi:ApiBaseAddress"]);
    })
    .AddClientAccessTokenHandler("reddit-api");

var app = builder.Build();

app.MapGet("/random", async ([FromServices] IRedditApi redditApi) => await redditApi.GetHotAsync());
app.MapGet("/history", () => "Hello World history!");

app.Run();
