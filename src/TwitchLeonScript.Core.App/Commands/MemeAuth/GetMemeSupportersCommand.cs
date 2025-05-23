using MediatR;
using TwitchLeonScript.Core.App.Responses.MemeAuth;

namespace TwitchLeonScript.Core.App.Commands.MemeAuth
{
    public class GetMemeSupportersCommand : IRequest<GetMemeSupportersResponse>
    {
        public required string OAuthToken { get; init; }
    }
}
