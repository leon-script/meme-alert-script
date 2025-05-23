using TwitchLeonScript.Core.Meme.Models;

namespace TwitchLeonScript.Core.App.Responses.MemeAuth
{
    public class GetMemeSupportersResponse
    {
        public required List<MemeSupporter> Supporters { get; init; }
    }
}
