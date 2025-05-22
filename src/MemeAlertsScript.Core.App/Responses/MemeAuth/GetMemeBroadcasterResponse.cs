using MemeAlertsScript.Core.Meme.Models;

namespace MemeAlertsScript.Core.App.Responses.MemeAuth
{
    public class GetMemeBroadcasterResponse
    {
        public required MemeBroadcaster Broadcaster { get; init; }
    }
}
