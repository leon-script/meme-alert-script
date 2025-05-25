using TwitchLeonScript.Core.Meme.Models;

namespace TwitchLeonScript.Core.App.Queries.MemeSupporters
{
    public class GetMemeSupportersResponse
    {
        public List<MemeSupporterDto> Supporters { get; init; } = new List<MemeSupporterDto>();
    }
}
