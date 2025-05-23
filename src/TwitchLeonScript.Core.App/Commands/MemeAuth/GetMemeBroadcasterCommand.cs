using MediatR;
using TwitchLeonScript.Core.App.Responses.MemeAuth;

namespace TwitchLeonScript.Core.App.Commands.MemeAuth
{
    public class GetMemeBroadcasterCommand : IRequest<GetMemeBroadcasterResponse>
    {
        public required string OAuthToken { get; init; }
    }
}
