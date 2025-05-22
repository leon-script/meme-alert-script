using MemeAlertsScript.Core.Common.Options;
using Microsoft.Extensions.Options;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus;

namespace MemeAlertsScript.Core.Twitch.Services
{
    public class TwitchRedemptionService
    {
        private readonly TwitchAPI _twitchApi;

        public TwitchRedemptionService(IOptions<TwitchOptions> options)
        {
            _twitchApi = new TwitchAPI();
            _twitchApi.Settings.ClientId = options.Value.AppId;
        }

        public Task<UpdateRedemptionStatusResponse> UpdateSingleRedemptionStatusAsync(
           string oauthToken,
           string broadcasterId,
           string rewardId,
           string redemptionId,
           CustomRewardRedemptionStatus status)
        {
            var redemptionIds = new List<string> { redemptionId };
            return UpdateRedemptionStatusAsync(oauthToken, broadcasterId, rewardId, redemptionIds, status);
        }

        public async Task<UpdateRedemptionStatusResponse> UpdateRedemptionStatusAsync(
            string oauthToken,
            string broadcasterId,
            string rewardId,
            List<string> redemptionIds,
            CustomRewardRedemptionStatus status)
        {
            var request = new UpdateCustomRewardRedemptionStatusRequest
            {
                Status = status
            };

            _twitchApi.Settings.AccessToken = oauthToken;
            return await _twitchApi.Helix.ChannelPoints.UpdateRedemptionStatusAsync(broadcasterId, rewardId, redemptionIds, request);
        }
    }
}
