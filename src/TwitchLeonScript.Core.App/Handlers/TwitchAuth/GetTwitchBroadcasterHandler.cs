using MediatR;
using TwitchLeonScript.Core.App.Commands.TwitchAuth;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Handlers.TwitchAuth
{
    public class GetTwitchBroadcasterHandler(
        TwitchAuthService twitchAuthService)
        : IRequestHandler<GetTwitchBroadcasterCommand, GetTwitchBroadcasterResponse>
    {
        public readonly TwitchAuthService _twitchAuthService = twitchAuthService;

        public async Task<GetTwitchBroadcasterResponse> Handle(GetTwitchBroadcasterCommand request, CancellationToken cancellationToken)
        {
            var broadcater = await _twitchAuthService.GetBroadcasterAsync(request.OAuthToken);

            return new GetTwitchBroadcasterResponse
            {
                Broadcaster = broadcater
            };
        }
    }
}
