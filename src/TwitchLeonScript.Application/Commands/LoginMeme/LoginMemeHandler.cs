using MediatR;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Application.Commands.LoginMeme
{
    public class LoginMemeHandler(
        IMemeBroadcasterApiService broadcasterApiService,
        ITwitchEventListener eventListener,
        ITwitchAuthContext twitchContext,
        IMemeAuthContext memeContext,
        IOptions<TwitchOptions> options)
        : IRequestHandler<LoginMemeCommand, Result<LoginMemeResponse>>
    {
        public async Task<Result<LoginMemeResponse>> Handle(LoginMemeCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.RefreshToken);

            try
            {
                var oauthToken = new MemeOAuthToken
                {
                    Token = request.OAuthToken,
                    RefreshToken = request.RefreshToken,
                    ExpiresAt = request.ExpiresAt ?? DateTime.UtcNow.AddHours(1),
                };
                var broadcaster = await broadcasterApiService.GetBroadcasterAsync(oauthToken.Token).ConfigureAwait(false);

                memeContext.Set(oauthToken, broadcaster);

                if (!eventListener.IsRunning && twitchContext.IsLogged && memeContext.IsLogged)
                {
                    await eventListener.StartAsync(options.Value.AppId, twitchContext.AppToken!, twitchContext.OAuthToken!, twitchContext.BroadcasterId!).ConfigureAwait(false);
                }

                return Result<LoginMemeResponse>.Success(new LoginMemeResponse
                {
                    OAuthToken = oauthToken,
                    Broadcaster = broadcaster,
                });
            }
            catch (Exception ex)
            {
                memeContext.Clear();
                return Result<LoginMemeResponse>.Failure(ex.Message);
            }
        }
    }
}
