using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Infrastructure.Contexts
{
    public sealed class MemeAuthContext : IMemeAuthContext
    {
        private MemeOAuthToken? _oauthToken;
        private MemeBroadcaster? _broadcaster;

        public bool IsLogged => _oauthToken is not null &&
                                _broadcaster is not null;

        public string? OAuthToken => IsLogged ? _oauthToken?.Token : null;
        public string? OAuthRefreshToken => IsLogged ? _oauthToken?.RefreshToken : null;
        public DateTime? OAuthExpiresAt => IsLogged ? _oauthToken?.ExpiresAt : null;

        public string? BroadcasterId => IsLogged ? _broadcaster?.Id : null;
        public string? BroadcasterLogin => IsLogged ? _broadcaster?.Login : null;

        public void Set(MemeOAuthToken oauthToken, MemeBroadcaster broadcaster)
        {
            _oauthToken = oauthToken;
            _broadcaster = broadcaster;
        }

        public void Clear()
        {
            _oauthToken = null;
            _broadcaster = null;
        }
    }
}
