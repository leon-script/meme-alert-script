namespace TwitchLeonScript.Core.Meme.Models
{
    public sealed class MemeRewardDto
    {
        public required string BroadcasterId { get; init; }
        public required string Title { get; init; }
        public required string Prompt { get; init; }
        public required int TwitchCost { get; init; }
        public required int MemeCost { get; init; }
    }
}
