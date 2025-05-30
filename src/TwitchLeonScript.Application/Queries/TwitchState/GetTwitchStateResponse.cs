namespace TwitchLeonScript.Application.Queries.TwitchState
{
    public sealed class GetTwitchStateResponse
    {
        public required string State { get; init; }
        public required Uri OAuthUri { get; init; }
        public required string RedirectUri { get; init; }
    }
}
