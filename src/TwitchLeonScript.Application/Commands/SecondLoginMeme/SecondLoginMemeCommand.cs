using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Application.Commands.SecondLoginMeme
{
    public sealed class SecondLoginMemeCommand : IRequest<Result<SecondLoginMemeResponse>>
    {
        public required MemeOAuthToken OAuthToken { get; init; }
        public required MemeBroadcaster Broadcaster { get; init; }
    }
}
