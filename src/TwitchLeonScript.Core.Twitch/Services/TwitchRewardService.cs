using TwitchLeonScript.Core.Common.Options;
using Microsoft.Extensions.Options;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;

namespace TwitchLeonScript.Core.Twitch.Services
{
    public class TwitchRewardService
    {
        private readonly TwitchAPI twitchApi;

        public TwitchRewardService(IOptions<TwitchOptions> options)
        {
            this.twitchApi = new TwitchAPI();
            this.twitchApi.Settings.ClientId = options.Value.AppId;
        }

        public async Task<List<CustomReward>> GetCustomRewardsAsync(
            string oauthToken,
            string broadcasterId,
            bool onlyManageableRewards = false)
        {
            this.twitchApi.Settings.AccessToken = oauthToken;

            var response = await this.twitchApi.Helix.ChannelPoints.GetCustomRewardAsync(
                broadcasterId: broadcasterId,
                onlyManageableRewards: onlyManageableRewards
            );

            return response.Data.ToList();
        }

        public async Task<CustomReward> CreateCustomRewardAsync(
            string oauthToken,
            string broadcasterId,
            string title,
            string prompt,
            int twitchCost,
            bool isEnabled,
            bool isUserInputRequired)
        {
            var request = new CreateCustomRewardsRequest
            {
                Title = title,
                Cost = twitchCost,
                Prompt = prompt,
                IsEnabled = isEnabled,
                IsUserInputRequired = isUserInputRequired
            };

            this.twitchApi.Settings.AccessToken = oauthToken;
            var response = await this.twitchApi.Helix.ChannelPoints.CreateCustomRewardsAsync(broadcasterId, request);
            return response.Data.First();
        }
    }
}
