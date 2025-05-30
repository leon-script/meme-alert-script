using AutoMapper;
using TwitchLeonScript.Domain.Enums;
using TwitchLib.Api.Core.Enums;

namespace TwitchLeonScript.Mapping.Converters
{
    internal sealed class RewardStatusConverter : ITypeConverter<CustomRewardRedemptionStatus, RedemptionStatus>
    {
        public RedemptionStatus Convert(CustomRewardRedemptionStatus source, RedemptionStatus destination, ResolutionContext context)
        {
            return source switch
            {
                CustomRewardRedemptionStatus.UNFULFILLED => RedemptionStatus.Unfulfilled,
                CustomRewardRedemptionStatus.FULFILLED => RedemptionStatus.Fulfilled,
                CustomRewardRedemptionStatus.CANCELED => RedemptionStatus.Canceled,
                _ => throw new ArgumentOutOfRangeException(nameof(source))
            };
        }
    }
}
