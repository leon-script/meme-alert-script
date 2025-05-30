namespace TwitchLeonScript.Domain.Models
{
    public sealed record TwitchBroadcaster
    {
        public required string Id { get; init; }
        public required string Login { get; init; }
    }
}
