using TwitchLeonScript.Core.Meme.Models;

namespace TwitchLeonScript.Core.App.Queries.MemeBroadcaster
{
    public sealed class GetMemeBroadcasterResponse
    {
        public MemeBroadcasterDto? Broadcaster { get; init; }
    }
}
