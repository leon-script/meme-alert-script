using MediatR;
using MemeAlertsScript.Core.App.Responses.TwitchAuth;

namespace MemeAlertsScript.Core.App.Commands.TwitchAuth
{
    public class GetTwitchAppTokenCommand : IRequest<GetTwitchAppTokenResponse>
    {
    }
}
