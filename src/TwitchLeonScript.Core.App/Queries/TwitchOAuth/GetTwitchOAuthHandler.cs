using MediatR;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Queries.TwitchOAuth
{
    public sealed class GetTwitchOAuthHandler(
        TwitchAuthService twitchOAuthService)
        : IRequestHandler<GetTwitchOAuthCommand, GetTwitchOAuthResponse>
    {
        public readonly TwitchAuthService _twitchOAuthService = twitchOAuthService;

        public async Task<GetTwitchOAuthResponse> Handle(GetTwitchOAuthCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Code);

            var oauthToken = await _twitchOAuthService.GetOAuthTokenAsync(request.Code);

            return new GetTwitchOAuthResponse
            {
                OAuthToken = oauthToken
            };
        }
    }
}