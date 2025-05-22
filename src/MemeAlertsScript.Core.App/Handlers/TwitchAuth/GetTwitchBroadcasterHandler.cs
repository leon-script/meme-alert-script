using MediatR;
using MemeAlertsScript.Core.App.Commands.TwitchAuth;
using MemeAlertsScript.Core.App.Responses.TwitchAuth;
using MemeAlertsScript.Core.Twitch.Services;

namespace MemeAlertsScript.Core.App.Handlers.TwitchAuth
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
