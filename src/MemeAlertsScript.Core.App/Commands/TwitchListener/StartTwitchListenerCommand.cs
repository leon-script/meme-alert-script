using MediatR;
using MemeAlertsScript.Core.App.Responses.TwitchListener;

namespace MemeAlertsScript.Core.App.Commands.TwitchListener
{
    public class StartTwitchListenerCommand : IRequest<StartTwitchListenerResponse>
    {
        public required string AccessToken { get; init; }
        public required string OAuthToken { get; init; }
        public required string BroadcasterId { get; init; }
    }
}
