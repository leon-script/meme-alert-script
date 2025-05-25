namespace TwitchLeonScript.Core.App.Commands.TwitchRedemption
{
    public sealed class UpdateTwitchRedemptionResponse
    {
        public required bool IsSuccess { get; init; }
        public string? RedemptionId { get; init; }
    }
}
