using TwitchLeonScript.Core.Meme.Models;

namespace TwitchLeonScript.Core.App.Responses.MemeAuth
{
    public class GetMemeBroadcasterResponse
    {
        public required MemeBroadcaster Broadcaster { get; init; }
    }
}
