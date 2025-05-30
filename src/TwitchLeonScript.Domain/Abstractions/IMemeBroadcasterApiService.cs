using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface IMemeBroadcasterApiService
    {
        Task<MemeBroadcaster> GetBroadcasterAsync(string oauthToken);
    }
}
