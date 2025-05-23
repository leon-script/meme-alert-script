using MediatR;
using TwitchLeonScript.Core.App.Commands.TwitchAuth;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;
using TwitchLeonScript.Core.Common.Options;
using TwitchLeonScript.Core.Twitch.Helpers;
using Microsoft.Extensions.Options;

namespace TwitchLeonScript.Core.App.Handlers.TwitchAuth
{
    public class GetTwitchStateHandler(
        IOptions<TwitchOptions> options)
        : IRequestHandler<GetTwitchStateCommand, GetTwitchStateResponse>
    {
        private readonly TwitchOptions _options = options.Value;

        public Task<GetTwitchStateResponse> Handle(GetTwitchStateCommand request, CancellationToken cancellationToken)
        {
            var state = TwitchOAuthHelper.CreateOAuthState();
            var uri = TwitchOAuthHelper.CreateOAuthUrl(_options.AppId, _options.RedirectUri, _options.Scopes, state);

            return Task.FromResult(new GetTwitchStateResponse
            {
                State = state,
                Uri = uri
            });
        }
    }
}
