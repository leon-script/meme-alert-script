using MediatR;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Application.Commands.SecondLoginMeme
{
    public class SecondLoginMemeHandler(
        ITwitchEventListener eventListener,
        ITwitchAuthContext twitchContext,
        IMemeAuthContext memeContext,
        IOptions<TwitchOptions> options)
        : IRequestHandler<SecondLoginMemeCommand, Result<SecondLoginMemeResponse>>
    {
        public async Task<Result<SecondLoginMemeResponse>> Handle(SecondLoginMemeCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.Broadcaster);

            try
            {
                memeContext.Set(request.OAuthToken, request.Broadcaster);

                if (!eventListener.IsRunning && twitchContext.IsLogged && memeContext.IsLogged)
                {
                    await eventListener.StartAsync(options.Value.AppId, twitchContext.AppToken!, twitchContext.OAuthToken!, twitchContext.BroadcasterId!).ConfigureAwait(false);
                }

                return Result<SecondLoginMemeResponse>.Success(new SecondLoginMemeResponse());
            }
            catch (Exception ex)
            {
                memeContext.Clear();
                return Result<SecondLoginMemeResponse>.Failure(ex.Message);
            }
        }
    }
}
