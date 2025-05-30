namespace TwitchLeonScript.Domain.Models
{
    public sealed class TwitchUser
    {
        public required string Id { get; init; }
        public required string Login { get; init; }
    }
}
