using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchRewardApiService
    {
        Task<IEnumerable<TwitchReward>> GetRewardsAsync();
        Task<TwitchReward> CreateRewardAsync(string title, int cost, string prompt);
    }
}
