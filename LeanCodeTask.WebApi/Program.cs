using LeanCodeTask.WebApi;
using LeanCodeTask.WebApi.Database;
using LeanCodeTask.WebApi.RedditApiClient;
using LeanCodeTask.WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRedditApiClient(builder.Configuration);
builder.Services.AddRedditImagesDatabase(builder.Configuration);
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();


var app = builder.Build();
app.UseEndpoints();
app.Run();