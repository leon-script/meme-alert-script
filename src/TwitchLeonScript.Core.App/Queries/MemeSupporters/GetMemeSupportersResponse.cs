using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Queries.MemeSupporters
{
    public class GetMemeSupportersResponse
    {
        public List<MemeSupporterDto> Supporters { get; init; } = [];
    }
}
