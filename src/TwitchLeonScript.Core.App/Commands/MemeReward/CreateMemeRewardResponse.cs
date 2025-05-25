namespace TwitchLeonScript.Core.App.Commands.MemeReward
{
    public sealed class CreateMemeRewardResponse
    {
        public required bool IsSuccess { get; init; }
        public string? MemeRewardId { get; init; }
    }
}
