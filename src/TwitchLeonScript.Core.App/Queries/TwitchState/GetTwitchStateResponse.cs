namespace TwitchLeonScript.Core.App.Queries.TwitchState
{
    public sealed class GetTwitchStateResponse
    {
        public required string State { get; init; }
        public required Uri Uri { get; init; }
    }
}
