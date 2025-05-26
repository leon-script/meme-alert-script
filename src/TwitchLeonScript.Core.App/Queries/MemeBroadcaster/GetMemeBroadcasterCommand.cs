using MediatR;

namespace TwitchLeonScript.Core.App.Queries.MemeBroadcaster
{
    public sealed class GetMemeBroadcasterCommand : IRequest<GetMemeBroadcasterResponse>
    {
        public string? OAuthToken { get; init; }
    }
}
