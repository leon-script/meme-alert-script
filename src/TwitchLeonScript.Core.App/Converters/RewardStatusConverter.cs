using AutoMapper;
using TwitchLeonScript.Core.App.Enums;
using TwitchLib.Api.Core.Enums;

namespace TwitchLeonScript.Core.App.Converters
{
    internal sealed class RewardStatusConverter : ITypeConverter<CustomRewardRedemptionStatus, TwitchRewardRedemptionStatus>
    {
        public TwitchRewardRedemptionStatus Convert(CustomRewardRedemptionStatus source, TwitchRewardRedemptionStatus destination, ResolutionContext context)
        {
            return source switch
            {
                CustomRewardRedemptionStatus.UNFULFILLED => TwitchRewardRedemptionStatus.Unfulfilled,
                CustomRewardRedemptionStatus.FULFILLED => TwitchRewardRedemptionStatus.Fulfilled,
                CustomRewardRedemptionStatus.CANCELED => TwitchRewardRedemptionStatus.Canceled,
                _ => throw new ArgumentOutOfRangeException(nameof(source))
            };
        }
    }
}
