using MediatR;
using MemeAlertsScript.Core.App.Responses.TwitchAuth;

namespace MemeAlertsScript.Core.App.Commands.TwitchAuth
{
    public class GetTwitchOAuthCommand : IRequest<GetTwitchOAuthResponse>
    {
        public required string Code { get; init; }
    }
}
