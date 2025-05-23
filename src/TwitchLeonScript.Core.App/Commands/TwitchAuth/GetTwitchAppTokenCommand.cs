using MediatR;
using TwitchLeonScript.Core.App.Responses.TwitchAuth;

namespace TwitchLeonScript.Core.App.Commands.TwitchAuth
{
    public class GetTwitchAppTokenCommand : IRequest<GetTwitchAppTokenResponse>
    {
    }
}
