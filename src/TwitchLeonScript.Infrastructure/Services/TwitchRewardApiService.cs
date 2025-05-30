using AutoMapper;
using Microsoft.Extensions.Options;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Domain.Options;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;

namespace TwitchLeonScript.Infrastructure.Services
{
    public class TwitchRewardApiService : ITwitchRewardApiService
    {
        private readonly ITwitchAuthContext _authContext;
        private readonly IMapper _mapper;
        private readonly TwitchAPI _twitchApi;

        public TwitchRewardApiService(
            ITwitchAuthContext authContext,
            IOptions<TwitchOptions> options,
            IMapper mapper)
        {
            _authContext = authContext;
            _mapper = mapper;

            _twitchApi = new TwitchAPI
            {
                Settings = { ClientId = options.Value.AppId }
            };
        }

        public async Task<IEnumerable<TwitchReward>> GetRewardsAsync()
        {
            _twitchApi.Settings.AccessToken = _authContext.OAuthToken;

            var response = await _twitchApi.Helix.ChannelPoints.GetCustomRewardAsync(
                broadcasterId: _authContext.BroadcasterId,
                onlyManageableRewards: true
            ).ConfigureAwait(false);

            return _mapper.Map<IEnumerable<TwitchReward>>(response.Data);
        }

        public async Task<TwitchReward> CreateRewardAsync(string title, int cost, string prompt)
        {
            _twitchApi.Settings.AccessToken = _authContext.OAuthToken;

            var request = new CreateCustomRewardsRequest
            {
                Title = title,
                Cost = cost,
                Prompt = prompt,
                IsEnabled = true,
                IsUserInputRequired = true,
            };

            var response = await _twitchApi.Helix.ChannelPoints.CreateCustomRewardsAsync(_authContext.BroadcasterId, request).ConfigureAwait(false);

            return _mapper.Map<TwitchReward>(response.Data.First());
        }
    }
}
