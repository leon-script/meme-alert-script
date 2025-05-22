using MediatR;
using MemeAlertsScript.Core.App.Responses.TwitchAuth;

namespace MemeAlertsScript.Core.App.Commands.TwitchAuth
{
    public class GetTwitchBroadcasterCommand : IRequest<GetTwitchBroadcasterResponse>
    {
        public required string OAuthToken { get; init; }
    }
}
