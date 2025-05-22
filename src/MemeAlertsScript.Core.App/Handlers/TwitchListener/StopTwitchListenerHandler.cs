
using MediatR;
using MemeAlertsScript.Core.App.Commands.TwitchListener;
using MemeAlertsScript.Core.App.Responses.TwitchListener;
using MemeAlertsScript.Core.Twitch.Listeners;

namespace MemeAlertsScript.Core.App.Handlers.TwitchListener
{
    public class StopTwitchListenerHandler(
        TwitchWebsocketListener twitchListener)
        : IRequestHandler<StopTwitchListenerCommand, StopTwitchListenerResponse>
    {
        private readonly TwitchWebsocketListener _twitchListener = twitchListener;

        public async Task<StopTwitchListenerResponse> Handle(StopTwitchListenerCommand request, CancellationToken cancellationToken)
        {
            await _twitchListener.StopAsync();
            
            return new StopTwitchListenerResponse();
        }
    }
}
