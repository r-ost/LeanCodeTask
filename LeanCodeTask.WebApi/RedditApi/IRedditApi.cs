using Refit;

namespace LeanCodeTask.WebApi.RedditApi
{
    [Headers(
        "Authorization: Bearer", 
        "User-Agent: Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36")]
    public interface IRedditApi
    {
        [Get("/r/bitcoin/hot")]
        Task<object> GetHotAsync();
    }
}