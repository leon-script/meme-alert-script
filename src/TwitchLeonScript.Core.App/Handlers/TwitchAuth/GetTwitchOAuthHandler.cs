using MediatR;
using TwitchLeonScript.Core.App.Commands.TwitchAuth;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Handlers.Twitch
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