using MediatR;
using MemeAlertsScript.Core.App.Commands.TwitchAuth;
using MemeAlertsScript.Core.App.Responses.TwitchAuth;
using MemeAlertsScript.Core.Twitch.Services;

namespace MemeAlertsScript.Core.App.Handlers.Twitch
{
    public class GetTwitchOAuthHandler(
        TwitchAuthService twitchOAuthService)
        : IRequestHandler<GetTwitchOAuthCommand, GetTwitchOAuthResponse>
    {
        public readonly TwitchAuthService _twitchOAuthService = twitchOAuthService;

        public async Task<GetTwitchOAuthResponse> Handle(GetTwitchOAuthCommand request, CancellationToken cancellationToken)
        {
            var oauthToken = await _twitchOAuthService.GetOAuthTokenAsync(request.Code);

            return new GetTwitchOAuthResponse
            {
                OAuthToken = oauthToken!
            };
        }
    }
}