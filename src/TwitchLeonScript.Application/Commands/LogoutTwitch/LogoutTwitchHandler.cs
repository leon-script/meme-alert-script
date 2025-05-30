using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Application.Commands.LogoutTwitch
{
    public class LogoutTwitchHandler(
        ITwitchEventListener eventListener,
        ITwitchAuthContext twitchContext)
        : IRequestHandler<LogoutTwitchCommand, Result<LogoutTwitchResponse>>
    {
        public async Task<Result<LogoutTwitchResponse>> Handle(LogoutTwitchCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            try
            {
                twitchContext.Clear();

                await eventListener.StopAsync().ConfigureAwait(false);

                return Result<LogoutTwitchResponse>.Success(new LogoutTwitchResponse());
            }
            catch (Exception ex)
            {
                return Result<LogoutTwitchResponse>.Failure(ex.Message);
            }
        }
    }
}
