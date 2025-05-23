
using MediatR;
using TwitchLeonScript.Core.App.Commands.TwitchListener;
using TwitchLeonScript.Core.App.Responses.TwitchListener;
using TwitchLeonScript.Core.Twitch.Listeners;

namespace TwitchLeonScript.Core.App.Handlers.TwitchListener
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
