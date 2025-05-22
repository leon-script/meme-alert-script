using MediatR;
using MemeAlertsScript.Core.App.Responses.MemeAuth;

namespace MemeAlertsScript.Core.App.Commands.MemeAuth
{
    public class GetMemeBroadcasterCommand : IRequest<GetMemeBroadcasterResponse>
    {
        public required string OAuthToken { get; init; }
    }
}
