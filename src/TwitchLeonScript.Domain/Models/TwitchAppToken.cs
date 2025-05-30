namespace TwitchLeonScript.Domain.Models
{
    public sealed record TwitchAppToken
    {
        public required string Token { get; init; }
    }
}
