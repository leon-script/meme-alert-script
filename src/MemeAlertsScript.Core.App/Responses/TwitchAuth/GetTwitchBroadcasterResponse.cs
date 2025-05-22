using MemeAlertsScript.Core.Twitch.Models;

namespace MemeAlertsScript.Core.App.Responses.TwitchAuth
{
    public class GetTwitchBroadcasterResponse
    {
        public required TwitchBroadcaster Broadcaster { get; init; }
    }
}
