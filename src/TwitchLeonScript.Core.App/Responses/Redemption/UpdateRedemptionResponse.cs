using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus;

namespace TwitchLeonScript.Core.App.Responses.Redemption
{
    public class UpdateRedemptionResponse
    {
        public required UpdateRedemptionStatusResponse StatusResponse { get; init; }
    }
}
