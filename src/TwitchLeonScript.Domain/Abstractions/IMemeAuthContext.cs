using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface IMemeAuthContext
    {
        bool IsLogged { get; }

        string? OAuthToken { get; }
        string? OAuthRefreshToken { get; }
        DateTime? OAuthExpiresAt { get; }

        string? BroadcasterId { get; }
        string? BroadcasterLogin { get; }

        void Set(MemeOAuthToken oauthToken, MemeBroadcaster broadcaster);
        void Clear();
    }
}
