using MediatR;
using TwitchLeonScript.Core.App.Services;

namespace TwitchLeonScript.Core.App.Queries.MemeBroadcaster
{
    public sealed class GetMemeBroadcasterHandler(
        MemeBroadcasterService memeAuthService)
        : IRequestHandler<GetMemeBroadcasterCommand, GetMemeBroadcasterResponse>
    {
        public readonly MemeBroadcasterService _memeAuthService = memeAuthService;

        public async Task<GetMemeBroadcasterResponse> Handle(GetMemeBroadcasterCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);

            var broadcaster = await _memeAuthService.GetBroadcasterAsync(request.OAuthToken);

            return new GetMemeBroadcasterResponse
            {
                Broadcaster = broadcaster,
            };
        }
    }
}
