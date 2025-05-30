using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchOAuthTokenApiService
    {
        Task<TwitchOAuthToken> GetOAuthTokenAsync(string code);
    }
}
