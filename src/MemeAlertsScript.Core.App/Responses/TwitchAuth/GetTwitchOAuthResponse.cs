using MemeAlertsScript.Core.Twitch.Models;

namespace MemeAlertsScript.Core.App.Responses.TwitchAuth
{
    public class GetTwitchOAuthResponse
    {
        public required TwitchOAuthToken OAuthToken { get; set; }
    }
}
