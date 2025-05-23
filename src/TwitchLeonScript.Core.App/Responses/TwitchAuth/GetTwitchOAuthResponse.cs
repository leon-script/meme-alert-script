using TwitchLeonScript.Core.Twitch.Models;

namespace TwitchLeonScript.Core.App.Responses.TwitchAuth
{
    public class GetTwitchOAuthResponse
    {
        public required TwitchOAuthToken OAuthToken { get; set; }
    }
}
