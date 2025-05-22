using TwitchLib.Api.Helix.Models.ChannelPoints;

namespace MemeAlertsScript.Core.App.Responses.Reward
{
    public class CreateMemeRewardResponse
    {
        public required CustomReward Reward { get; init; }
    }
}
