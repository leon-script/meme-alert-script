using MediatR;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Core.App.Helpers;
using TwitchLeonScript.Core.App.Options;

namespace TwitchLeonScript.Core.App.Queries.TwitchState
{
    public sealed class GetTwitchStateHandler(
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
