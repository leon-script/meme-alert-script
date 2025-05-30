using AutoMapper;
using TwitchLeonScript.Domain.Enums;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Mapping.Converters;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Helix.Models.ChannelPoints;

namespace TwitchLeonScript.Mapping.Profiles
{
    public sealed class RewardProfile : Profile
    {
        public RewardProfile()
        {
            CreateMap<CustomReward, TwitchReward>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Prompt, opt => opt.MapFrom(src => src.Prompt))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));

            CreateMap<CustomRewardRedemptionStatus, RedemptionStatus>()
                .ConvertUsing(new RewardStatusConverter());
        }
    }
}
