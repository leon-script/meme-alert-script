using MediatR;
using MemeAlertsScript.Core.App.Commands.MemeAuth;
using MemeAlertsScript.Core.App.Responses.MemeAuth;
using MemeAlertsScript.Core.Meme.Services;

namespace MemeAlertsScript.Core.App.Handlers.Twitch
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
