using LeanCodeTask.WebApi.Database.Repositories;
using LeanCodeTask.WebApi.Utils;

namespace LeanCodeTask.WebApi.Database;

public static class RedditImagesDatabaseExtensions
{
    public static IServiceCollection AddRedditImagesDatabase(this IServiceCollection services, IConfiguration configuration)
    { 
        services.Configure<RedditImagesDatabaseSettings>(configuration.GetSection("RedditImagesDatabase"));
        
        services.AddSingleton<IRedditImagesRepository, RedditImagesRepository>();
        
        return services;
    }
}