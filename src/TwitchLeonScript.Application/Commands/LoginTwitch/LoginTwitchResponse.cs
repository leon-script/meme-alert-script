using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Application.Commands.LoginTwitch
{
    public sealed class LoginTwitchResponse
    {
        public required TwitchAppToken AppToken { get; init; }
        public required TwitchOAuthToken OAuthToken { get; init; }
        public required TwitchBroadcaster Broadcaster { get; init; }
    }
}
