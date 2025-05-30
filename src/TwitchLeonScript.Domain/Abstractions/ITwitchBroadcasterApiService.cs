using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchBroadcasterApiService
    {
        Task<TwitchBroadcaster> GetBroadcasterAsync(string oauthToken);
    }
}
