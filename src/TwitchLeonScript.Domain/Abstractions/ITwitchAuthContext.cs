using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchAuthContext
    {
        bool IsLogged { get; }

        string? AppToken { get; }

        string? OAuthToken { get; }
        string? OAuthRefreshToken { get; }
        DateTime? OAuthExpiresAt { get; }

        string? BroadcasterId { get; }
        string? BroadcasterLogin { get; }

        void Set(TwitchAppToken appToken, TwitchOAuthToken oauthToken, TwitchBroadcaster broadcaster);
        void Clear();
    }
}
