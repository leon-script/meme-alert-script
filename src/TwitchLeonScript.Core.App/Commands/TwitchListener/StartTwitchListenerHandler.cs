using MediatR;
using TwitchLeonScript.Core.Twitch.Listeners;

namespace TwitchLeonScript.Core.App.Commands.TwitchListener
{
    public sealed class StartTwitchListenerHandler(
        TwitchWebsocketListener twitchListener)
        : IRequestHandler<StartTwitchListenerCommand, StartTwitchListenerResponse>
    {
        private readonly TwitchWebsocketListener _twitchListener = twitchListener;

        public async Task<StartTwitchListenerResponse> Handle(StartTwitchListenerCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.AccessToken);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.BroadcasterId);

            var isSuccess = await _twitchListener.StartAsync(request.AccessToken, request.OAuthToken, request.BroadcasterId);

            return new StartTwitchListenerResponse 
            {
                IsSuccess = isSuccess,
                Unit = Unit.Value,
            };
        }
    }
}
