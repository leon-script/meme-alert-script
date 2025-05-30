using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Application.Commands.SecondLoginTwitch
{
    public sealed class SecondLoginTwitchCommand : IRequest<Result<SecondLoginTwitchResponse>>
    {
        public required TwitchAppToken AppToken { get; init; }
        public required TwitchOAuthToken OAuthToken { get; init; }
        public required TwitchBroadcaster Broadcaster { get; init; }
    }
}
