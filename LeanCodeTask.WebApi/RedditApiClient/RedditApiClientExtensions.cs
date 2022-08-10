using IdentityModel.Client;
using Refit;

namespace LeanCodeTask.WebApi.RedditApiClient;

public static class RedditApiClientExtensions
{
    public static IServiceCollection AddRedditApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAccessTokenManagement(options =>
        {
            options.Client.Clients.Add("reddit-api", new ClientCredentialsTokenRequest
            {
                RequestUri = new Uri(configuration["RedditApi:TokenEndpoint"]),
                ClientId = configuration["RedditApi:ClientId"],
                ClientSecret = configuration["RedditApi:ClientSecret"],
                GrantType = "client_credentials",
            });
        });
        
        services.AddRefitClient<IRedditApiClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration["RedditApi:ApiBaseAddress"]);
            })
            .AddClientAccessTokenHandler("reddit-api");

        services.Configure<RedditApiSettings>(configuration.GetSection("RedditApi"));

        return services;
    }
}