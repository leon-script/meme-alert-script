
using MediatR;
using TwitchLeonScript.Core.App.Commands.TwitchListener;
using TwitchLeonScript.Core.App.Responses.TwitchListener;
using TwitchLeonScript.Core.Twitch.Listeners;

namespace TwitchLeonScript.Core.App.Handlers.TwitchListener
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
