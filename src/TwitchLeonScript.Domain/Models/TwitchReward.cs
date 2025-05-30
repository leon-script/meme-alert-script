namespace TwitchLeonScript.Domain.Models
{
    public sealed class TwitchReward
    {
        public required string Id { get; init; }
        public required string Title { get; init; }
        public required int Cost { get; init; }
        public required string Prompt { get; init; }
    }
}
