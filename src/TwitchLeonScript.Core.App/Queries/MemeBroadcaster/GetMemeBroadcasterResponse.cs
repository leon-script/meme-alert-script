using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Queries.MemeBroadcaster
{
    public sealed class GetMemeBroadcasterResponse
    {
        public MemeBroadcasterDto? Broadcaster { get; init; }
    }
}
