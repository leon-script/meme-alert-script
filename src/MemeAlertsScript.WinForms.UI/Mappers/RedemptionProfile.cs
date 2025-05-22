using AutoMapper;
using MemeAlertsScript.Core.Common.Extensions;
using MemeAlertsScript.WinForms.UI.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace MemeAlertsScript.WinForms.UI.Mappers
{
    internal class RedemptionProfile : Profile
    {
        public RedemptionProfile()
        {
            CreateMap<ChannelPointsCustomRewardRedemption, MemeRedemptionGridRow>()
                .ForMember(dest => dest.RewardId, opt => opt.MapFrom(src => src.Reward.Id))
                .ForMember(dest => dest.RedemptionId, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Status, opt => opt.Ignore())
                //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Reward.Title))
                //.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                //.ForMember(dest => dest.Input, opt => opt.MapFrom(src => src.UserInput))
                //.ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.RedeemedAt.ToString("HH:mm:ss")))
                //.ForMember(dest => dest.MemePoints, opt => opt.MapFrom(src => src.Reward.Prompt.ParseMemeTagNumber()))
                ;
        }
    }
}
