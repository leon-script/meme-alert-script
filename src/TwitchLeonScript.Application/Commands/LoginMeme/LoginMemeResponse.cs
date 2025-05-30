using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Application.Commands.LoginMeme
{
    public sealed class LoginMemeResponse
    {
        public required MemeOAuthToken OAuthToken { get; init; }
        public required MemeBroadcaster Broadcaster { get; init; }
    }
}
