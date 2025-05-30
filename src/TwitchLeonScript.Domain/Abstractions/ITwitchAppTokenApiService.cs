using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchAppTokenApiService
    {
        Task<TwitchAppToken> GetAppTokenAsync();
    }
}
