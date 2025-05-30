using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Commands.LoginTwitch
{
    public sealed class LoginTwitchCommand : IRequest<Result<LoginTwitchResponse>>
    {
        public string? Code { get; init; }
    }
}
