namespace TwitchLeonScript.Domain.Models
{
    public sealed class MemeReward
    {
        public required string Id { get; init; }
        public required string Title { get; init; }
        public required int TwitchCost { get; init; }
        public required int MemeCost { get; init; }
        public required string Prompt { get; init; }
    }
}
