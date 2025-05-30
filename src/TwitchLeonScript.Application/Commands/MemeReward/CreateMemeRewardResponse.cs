using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Application.Commands.MemeReward
{
    public sealed class CreateMemeRewardResponse
    {
        public required TwitchReward Reward { get; init; }
    }
}
