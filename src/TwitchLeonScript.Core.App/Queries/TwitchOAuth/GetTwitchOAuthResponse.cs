using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Queries.TwitchOAuth
{
    public sealed class GetTwitchOAuthResponse
    {
        public TwitchOAuthTokenDto? OAuthToken { get; set; }
    }
}
