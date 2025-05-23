using MediatR;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;

namespace TwitchLeonScript.Core.App.Commands.TwitchAuth
{
    public class GetTwitchBroadcasterCommand : IRequest<GetTwitchBroadcasterResponse>
    {
        public required string OAuthToken { get; init; }
    }
}
