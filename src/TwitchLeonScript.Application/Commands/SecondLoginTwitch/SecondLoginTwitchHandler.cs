using MediatR;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Application.Commands.SecondLoginTwitch
{
    public class SecondLoginTwitchHandler(
        ITwitchEventListener eventListener,
        ITwitchAuthContext twitchContext,
        IMemeAuthContext memeContext,
        IOptions<TwitchOptions> options)
        : IRequestHandler<SecondLoginTwitchCommand, Result<SecondLoginTwitchResponse>>
    {
        public async Task<Result<SecondLoginTwitchResponse>> Handle(SecondLoginTwitchCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.AppToken);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.Broadcaster);

            try
            {
                twitchContext.Set(request.AppToken, request.OAuthToken, request.Broadcaster);

                if (!eventListener.IsRunning && twitchContext.IsLogged && memeContext.IsLogged)
                {
                    await eventListener.StartAsync(options.Value.AppId, twitchContext.AppToken!, twitchContext.OAuthToken!, twitchContext.BroadcasterId!).ConfigureAwait(false);
                }

                return Result<SecondLoginTwitchResponse>.Success(new SecondLoginTwitchResponse());
            }
            catch (Exception ex)
            {
                twitchContext.Clear();
                return Result<SecondLoginTwitchResponse>.Failure(ex.Message);
            }
        }
    }
}
