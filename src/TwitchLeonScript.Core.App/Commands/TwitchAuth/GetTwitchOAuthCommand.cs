using MediatR;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;

namespace TwitchLeonScript.Core.App.Commands.TwitchAuth
{
    public class GetTwitchOAuthCommand : IRequest<GetTwitchOAuthResponse>
    {
        public required string Code { get; init; }
    }
}
