using AutoMapper;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Enums;
using TwitchLeonScript.Domain.Options;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class TwitchRedemptionApiService : ITwitchRedemptionApiService
    {
        private readonly IMapper _mapper;
        private readonly ITwitchAuthContext _authContext;
        private readonly TwitchAPI _twitchApi;

        public TwitchRedemptionApiService(
            IMapper mapper,
            ITwitchAuthContext authContext,
            IOptions<TwitchOptions> options)
        {
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(authContext);
            ArgumentNullException.ThrowIfNull(options.Value);

            _mapper = mapper;
            _authContext = authContext;
            _twitchApi = new TwitchAPI
            {
                Settings = { ClientId = options.Value.AppId }
            };
        }

        public async Task UpdateRedemptionStatusAsync(string broadcasterId, string rewardId, string redemptionId, RedemptionStatus status)
        {
            ArgumentException.ThrowIfNullOrEmpty(broadcasterId);
            ArgumentException.ThrowIfNullOrEmpty(rewardId);
            ArgumentException.ThrowIfNullOrEmpty(redemptionId);

            var request = new UpdateCustomRewardRedemptionStatusRequest
            {
                Status = _mapper.Map<CustomRewardRedemptionStatus>(status)
            };

            _twitchApi.Settings.AccessToken = _authContext.OAuthToken;

            await _twitchApi.Helix.ChannelPoints.UpdateRedemptionStatusAsync(
                broadcasterId: broadcasterId,
                rewardId: rewardId,
                redemptionIds: [redemptionId],
                request: request).ConfigureAwait(false);
        }
    }
}
