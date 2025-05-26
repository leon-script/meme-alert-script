using AutoMapper;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Core.App.Options;
using TwitchLeonScript.Core.App.Models;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;

namespace TwitchLeonScript.Core.App.Services
{
    public sealed class TwitchRedemptionService
    {
        private readonly IMapper _mapper;
        private readonly TwitchAPI _twitchApi;

        public TwitchRedemptionService(IMapper mapper, IOptions<TwitchOptions> options)
        {
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(options?.Value?.AppId);

            _mapper = mapper;
            _twitchApi = new TwitchAPI
            {
                Settings = { ClientId = options.Value.AppId }
            };
        }

        public async Task<string?> UpdateRedemptionStatusAsync(string oauthToken, TwitchRedemptionDto redemption)
        {
            ArgumentException.ThrowIfNullOrEmpty(oauthToken);
            ArgumentNullException.ThrowIfNull(redemption);

            _twitchApi.Settings.AccessToken = oauthToken;

            var status = _mapper.Map<CustomRewardRedemptionStatus>(redemption.Status);
            var request = new UpdateCustomRewardRedemptionStatusRequest { Status = status };

            var response = await _twitchApi.Helix.ChannelPoints.UpdateRedemptionStatusAsync(
                redemption.BroadcasterId,
                redemption.RewardId,
                [redemption.RedemptionId],
                request);

            return response.Data?.FirstOrDefault()?.Id;
        }
    }
}
