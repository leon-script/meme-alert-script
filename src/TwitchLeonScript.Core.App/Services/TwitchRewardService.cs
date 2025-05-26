using TwitchLeonScript.Core.App.Options;
using Microsoft.Extensions.Options;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;

namespace TwitchLeonScript.Core.App.Services
{
    public class TwitchRewardService
    {
        private readonly TwitchAPI _twitchApi;

        public TwitchRewardService(IOptions<TwitchOptions> options)
        {
            _twitchApi = new TwitchAPI();
            _twitchApi.Settings.ClientId = options.Value.AppId;
        }

        public async Task<List<CustomReward>> GetCustomRewardsAsync(
            string oauthToken,
            string broadcasterId,
            bool onlyManageableRewards = false)
        {
            _twitchApi.Settings.AccessToken = oauthToken;

            var response = await _twitchApi.Helix.ChannelPoints.GetCustomRewardAsync(
                broadcasterId: broadcasterId,
                onlyManageableRewards: onlyManageableRewards
            );

            return [.. response.Data];
        }

        public async Task<CustomReward?> CreateCustomRewardAsync(
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

            _twitchApi.Settings.AccessToken = oauthToken;
            var response = await _twitchApi.Helix.ChannelPoints.CreateCustomRewardsAsync(broadcasterId, request);
            return response.Data.FirstOrDefault();
        }
    }
}
