using MediatR;

namespace TwitchLeonScript.Core.App.Queries.TwitchBroadcaster
{
    public sealed class GetTwitchBroadcasterCommand : IRequest<GetTwitchBroadcasterResponse>
    {
        public string? OAuthToken { get; init; }
    }
}
