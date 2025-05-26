using MediatR;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLeonScript.Core.App.Notifications
{
    public class TwitchRedemptionReceived(
        ChannelPointsCustomRewardRedemption redemption) 
        : INotification
    {
        public ChannelPointsCustomRewardRedemption Redemption { get; } = redemption;
    }
}