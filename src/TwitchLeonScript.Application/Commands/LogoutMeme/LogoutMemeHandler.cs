using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Application.Commands.LogoutMeme
{
    public class LogoutMemeHandler(
        ITwitchEventListener eventListener,
        IMemeAuthContext memeContext)
        : IRequestHandler<LogoutMemeCommand, Result<LogoutMemeResponse>>
    {
        public async Task<Result<LogoutMemeResponse>> Handle(LogoutMemeCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            try
            {
                memeContext.Clear();

                await eventListener.StopAsync().ConfigureAwait(false);

                return Result<LogoutMemeResponse>.Success(new LogoutMemeResponse());
            }
            catch (Exception ex)
            {
                return Result<LogoutMemeResponse>.Failure(ex.Message);
            }
        }
    }
}
