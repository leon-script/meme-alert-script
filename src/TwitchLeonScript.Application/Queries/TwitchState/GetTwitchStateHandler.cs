using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Application.Queries.TwitchState
{
    public sealed class GetTwitchStateHandler(
        ITwitchStateService stateService)
        : IRequestHandler<GetTwitchStateQuery, Result<GetTwitchStateResponse>>
    {
        public Task<Result<GetTwitchStateResponse>> Handle(GetTwitchStateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var state = stateService.GetOAuthState();
                var oauthUri = stateService.CreateOAuthUrl(state);
                var redirectUri = stateService.GetRedirectUrl();

                return Task.FromResult(Result<GetTwitchStateResponse>.Success(new GetTwitchStateResponse
                {
                    State = state,
                    OAuthUri = oauthUri,
                    RedirectUri = redirectUri
                }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<GetTwitchStateResponse>.Failure(ex.Message));
            }
        }
    }
}
