using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Commands.LogoutMeme
{
    public sealed class LogoutMemeCommand : IRequest<Result<LogoutMemeResponse>>
    {
    }
}
