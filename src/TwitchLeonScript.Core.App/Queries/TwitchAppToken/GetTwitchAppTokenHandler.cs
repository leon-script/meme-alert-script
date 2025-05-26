using MediatR;
using TwitchLeonScript.Core.App.Services;

namespace TwitchLeonScript.Core.App.Queries.TwitchAppToken
{
    public sealed class GetTwitchAppTokenHandler(
        TwitchAuthService twitchAuthService)
        : IRequestHandler<GetTwitchAppTokenCommand, GetTwitchAppTokenResponse>
    {
        public readonly TwitchAuthService _twitchAuthService = twitchAuthService;

        public async Task<GetTwitchAppTokenResponse> Handle(GetTwitchAppTokenCommand request, CancellationToken cancellationToken)
        {
            var accessToken = await _twitchAuthService.GetAppTokenAsync();

            return new GetTwitchAppTokenResponse
            {
               AccessToken = accessToken
            };
        }
    }
}
