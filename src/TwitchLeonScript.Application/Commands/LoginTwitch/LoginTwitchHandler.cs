using MediatR;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Application.Commands.LoginTwitch
{
    public class LoginTwitchHandler(
        ITwitchAppTokenApiService appTokenApiService,
        ITwitchOAuthTokenApiService oauthTokenApiService,
        ITwitchBroadcasterApiService broadcasterApiService,
        ITwitchEventListener eventListener,
        ITwitchAuthContext twitchContext,
        IMemeAuthContext memeContext,
        IOptions<TwitchOptions> options)
        : IRequestHandler<LoginTwitchCommand, Result<LoginTwitchResponse>>
    {
        public async Task<Result<LoginTwitchResponse>> Handle(LoginTwitchCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Code);

            try
            {
                var appToken = await appTokenApiService.GetAppTokenAsync().ConfigureAwait(false);
                var oauthToken = await oauthTokenApiService.GetOAuthTokenAsync(request.Code).ConfigureAwait(false);
                var broadcaster = await broadcasterApiService.GetBroadcasterAsync(oauthToken.Token).ConfigureAwait(false);

                twitchContext.Set(appToken, oauthToken, broadcaster);

                if (!eventListener.IsRunning && twitchContext.IsLogged && memeContext.IsLogged)
                {
                    await eventListener.StartAsync(options.Value.AppId, twitchContext.AppToken!, twitchContext.OAuthToken!, twitchContext.BroadcasterId!).ConfigureAwait(false);
                }

                return Result<LoginTwitchResponse>.Success(new LoginTwitchResponse
                {
                    AppToken = appToken,
                    OAuthToken = oauthToken,
                    Broadcaster = broadcaster,
                });
            }
            catch (Exception ex)
            {
                twitchContext.Clear();
                return Result<LoginTwitchResponse>.Failure(ex.Message);
            }
        }
    }
}
