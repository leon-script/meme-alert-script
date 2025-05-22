using MediatR;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace MemeAlertsScript.Core.Twitch.Notifications
{
    public class TwitchRedemptionReceived(
        ChannelPointsCustomRewardRedemption redemption) 
        : INotification
    {
        public ChannelPointsCustomRewardRedemption Redemption { get; } = redemption;
    }
}