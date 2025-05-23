using MediatR;
using TwitchLeonScript.Core.App.Commands.TwitchAuth;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Handlers.Twitch
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
