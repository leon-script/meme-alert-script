using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Infrastructure.Contexts
{
    public sealed class TwitchAuthContext : ITwitchAuthContext
    {
        private TwitchAppToken? _appToken;
        private TwitchOAuthToken? _oauthToken;
        private TwitchBroadcaster? _broadcaster;

        public bool IsLogged => _appToken is not null &&
                                _oauthToken is not null &&
                                _broadcaster is not null;

        public string? AppToken => IsLogged ? _appToken!.Token : null;

        public string? OAuthToken => IsLogged ? _oauthToken?.Token : null;
        public string? OAuthRefreshToken => IsLogged ? _oauthToken?.RefreshToken : null;
        public DateTime? OAuthExpiresAt => IsLogged ? _oauthToken?.ExpiresAt : null;

        public string? BroadcasterId => IsLogged ? _broadcaster?.Id : null;
        public string? BroadcasterLogin => IsLogged ? _broadcaster?.Login : null;

        public void Set(TwitchAppToken appToken, TwitchOAuthToken oauthToken, TwitchBroadcaster broadcaster)
        {
            _appToken = appToken;
            _oauthToken = oauthToken;
            _broadcaster = broadcaster;
        }

        public void Clear()
        {
            _appToken = null;
            _oauthToken = null;
            _broadcaster = null;
        }
    }
}
