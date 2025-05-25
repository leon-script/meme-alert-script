using TwitchLeonScript.Core.Twitch.Models;

namespace TwitchLeonScript.Core.App.Queries.TwitchOAuth
{
    public sealed class GetTwitchOAuthResponse
    {
        public TwitchOAuthTokenDto? OAuthToken { get; set; }
    }
}
