using MediatR;
using TwitchLeonScript.Core.App.Commands.MemeAuth;
using TwitchLeonScript.Core.App.Responses.MemeAuth;
using TwitchLeonScript.Core.Meme.Services;

namespace TwitchLeonScript.Core.App.Handlers.Twitch
{
    public class GetMemeBroadcasterHandler(
        MemeAuthService memeAuthService)
        : IRequestHandler<GetMemeBroadcasterCommand, GetMemeBroadcasterResponse>
    {
        public readonly MemeAuthService _memeAuthService = memeAuthService;

        public async Task<GetMemeBroadcasterResponse> Handle(GetMemeBroadcasterCommand request, CancellationToken cancellationToken)
        {
            var broadcaster = await _memeAuthService.GetBroadcasterAsync(request.OAuthToken);

            return new GetMemeBroadcasterResponse
            {
                Broadcaster = broadcaster
            };
        }
    }
}
