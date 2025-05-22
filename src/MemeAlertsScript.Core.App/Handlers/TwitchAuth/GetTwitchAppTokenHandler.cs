using MediatR;
using MemeAlertsScript.Core.App.Commands.TwitchAuth;
using MemeAlertsScript.Core.App.Responses.TwitchAuth;
using MemeAlertsScript.Core.Twitch.Services;

namespace MemeAlertsScript.Core.App.Handlers.Twitch
{
    public class GetTwitchAppTokenHandler(
        TwitchAuthService twitchAuthService)
        : IRequestHandler<GetTwitchAppTokenCommand, GetTwitchAppTokenResponse>
    {
        public readonly TwitchAuthService _twitchAuthService = twitchAuthService;

        public async Task<GetTwitchAppTokenResponse> Handle(GetTwitchAppTokenCommand request, CancellationToken cancellationToken)
        {
            var accessToken = await _twitchAuthService.GetAppTokenAsync();

            return new GetTwitchAppTokenResponse
            {
               AccessToken = accessToken!
            };
        }
    }
}
