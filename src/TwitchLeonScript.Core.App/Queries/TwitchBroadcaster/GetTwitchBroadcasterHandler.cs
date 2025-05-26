using MediatR;
using TwitchLeonScript.Core.App.Services;

namespace TwitchLeonScript.Core.App.Queries.TwitchBroadcaster
{
    public sealed class GetTwitchBroadcasterHandler(
        TwitchAuthService twitchAuthService)
        : IRequestHandler<GetTwitchBroadcasterCommand, GetTwitchBroadcasterResponse>
    {
        public readonly TwitchAuthService _twitchAuthService = twitchAuthService;

        public async Task<GetTwitchBroadcasterResponse> Handle(GetTwitchBroadcasterCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);

            var broadcater = await _twitchAuthService.GetBroadcasterAsync(request.OAuthToken);

            return new GetTwitchBroadcasterResponse
            {
                Broadcaster = broadcater
            };
        }
    }
}
