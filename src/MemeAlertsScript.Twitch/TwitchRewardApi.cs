using Microsoft.Extensions.Logging;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Helix.Models.ChannelPoints;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus;

namespace MemeAlertsScript.Twitch
{
    public class TwitchRewardApi
    {
        private readonly TwitchAPI _twitchApi;
        private readonly string _broadcasterId;
        private readonly ILogger _logger;

        public TwitchRewardApi(string appId, string oauthToken, string broadcasterId, ILogger logger)
        {
            _twitchApi = new TwitchAPI();
            _twitchApi.Settings.ClientId = appId;
            _twitchApi.Settings.AccessToken = oauthToken;

            _broadcasterId = broadcasterId;
            _logger = logger;
        }

        public async Task<List<CustomReward>> GetCustomRewardsAsync(string broadcasterId, bool onlyManageableRewards = false)
        {
            var response = await _twitchApi.Helix.ChannelPoints.GetCustomRewardAsync(
                broadcasterId: broadcasterId,
                onlyManageableRewards: onlyManageableRewards
            );

            return response.Data.ToList();
        }

        public async Task<CustomReward> CreateCustomRewardAsync(
            string title,
            int cost,
            string prompt = "",
            bool isEnabled = true,
            bool isUserInputRequired = false)
        {
            var request = new CreateCustomRewardsRequest
            {
                Title = title,
                Cost = cost,
                Prompt = prompt,
                IsEnabled = isEnabled,
                IsUserInputRequired = isUserInputRequired
            };

            var response = await _twitchApi.Helix.ChannelPoints.CreateCustomRewardsAsync(_broadcasterId, request);
            return response.Data.First();
        }

        public async Task<UpdateRedemptionStatusResponse> UpdateRedemptionStatusAsync(
            string rewardId,
            List<string> redemptionIds,
            CustomRewardRedemptionStatus status)
        {
            var request = new UpdateCustomRewardRedemptionStatusRequest
            {
                Status = status
            };

            return await _twitchApi.Helix.ChannelPoints.UpdateRedemptionStatusAsync(_broadcasterId, rewardId, redemptionIds, request);
        }

        public async Task DeleteCustomRewardAsync(string rewardId)
        {
            await _twitchApi.Helix.ChannelPoints.DeleteCustomRewardAsync(_broadcasterId, rewardId);
        }

        public Task<UpdateRedemptionStatusResponse> UpdateSingleRedemptionStatusAsync(
            string rewardId,
            string redemptionId,
            CustomRewardRedemptionStatus status)
        {
            var redemptionIds = new List<string> { redemptionId };
            return UpdateRedemptionStatusAsync(rewardId, redemptionIds, status);
        }
    }
}
