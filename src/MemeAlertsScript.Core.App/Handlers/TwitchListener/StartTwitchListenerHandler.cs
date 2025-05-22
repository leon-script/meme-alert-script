
using MediatR;
using MemeAlertsScript.Core.App.Commands.TwitchListener;
using MemeAlertsScript.Core.App.Responses.TwitchListener;
using MemeAlertsScript.Core.Twitch.Listeners;

namespace MemeAlertsScript.Core.App.Handlers.TwitchListener
{
    public class StartTwitchListenerHandler(
        TwitchWebsocketListener twitchListener)
        : IRequestHandler<StartTwitchListenerCommand, StartTwitchListenerResponse>
    {
        private readonly TwitchWebsocketListener _twitchListener = twitchListener;

        public async Task<StartTwitchListenerResponse> Handle(StartTwitchListenerCommand request, CancellationToken cancellationToken)
        {
            await _twitchListener.StartAsync(request.AccessToken, request.OAuthToken, request.BroadcasterId);
            
            return new StartTwitchListenerResponse
            {
                Unit = Unit.Value
            };
        }
    }
}
