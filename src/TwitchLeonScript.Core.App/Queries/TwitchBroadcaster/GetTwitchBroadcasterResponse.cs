using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Queries.TwitchBroadcaster
{
    public sealed class GetTwitchBroadcasterResponse
    {
        public TwitchBroadcasterDto? Broadcaster { get; init; }
    }
}
