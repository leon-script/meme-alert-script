using MediatR;
using TwitchLeonScript.Core.App.Queries.MemeBroadcaster;

namespace TwitchLeonScript.Core.App.Queries.MemeAuth
{
    public sealed class GetMemeBroadcasterCommand : IRequest<GetMemeBroadcasterResponse>
    {
        public string? OAuthToken { get; init; }
    }
}
