
using MediatR;
using TwitchLeonScript.Core.Twitch.Listeners;

namespace TwitchLeonScript.Core.App.Commands.TwitchListener
{
    public sealed class StopTwitchListenerHandler(
        TwitchWebsocketListener twitchListener)
        : IRequestHandler<StopTwitchListenerCommand, StopTwitchListenerResponse>
    {
        private readonly TwitchWebsocketListener _twitchListener = twitchListener;

        public async Task<StopTwitchListenerResponse> Handle(StopTwitchListenerCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _twitchListener.StopAsync();

            return new StopTwitchListenerResponse() { IsSuccess = isSuccess };
        }
    }
}
