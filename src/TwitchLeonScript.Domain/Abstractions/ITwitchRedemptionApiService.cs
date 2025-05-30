using TwitchLeonScript.Domain.Enums;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchRedemptionApiService
    {
        Task UpdateRedemptionStatusAsync(string broadcasterId, string rewardId, string redemptionId, RedemptionStatus status);
    }
}
