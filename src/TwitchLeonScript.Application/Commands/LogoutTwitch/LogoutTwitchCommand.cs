using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Commands.LogoutTwitch
{
    public sealed class LogoutTwitchCommand : IRequest<Result<LogoutTwitchResponse>>
    {
    }
}
