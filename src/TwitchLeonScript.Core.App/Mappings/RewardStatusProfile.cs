using AutoMapper;
using TwitchLeonScript.Core.App.Converters;
using TwitchLeonScript.Core.Common.Enums;
using TwitchLib.Api.Core.Enums;

namespace TwitchLeonScript.Core.App.Mappings
{
    internal sealed class RewardStatusProfile : Profile
    {
        public RewardStatusProfile()
        {
            CreateMap<CustomRewardRedemptionStatus, TwitchRewardRedemptionStatus>()
                .ConvertUsing(new RewardStatusConverter());
        }
    }
}
