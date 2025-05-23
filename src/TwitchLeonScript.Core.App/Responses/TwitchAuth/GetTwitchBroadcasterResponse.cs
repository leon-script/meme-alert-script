using TwitchLeonScript.Core.Twitch.Models;

namespace TwitchLeonScript.Core.App.Responses.TwitchAuth
{
    public class GetTwitchBroadcasterResponse
    {
        public required TwitchBroadcaster Broadcaster { get; init; }
    }
}
