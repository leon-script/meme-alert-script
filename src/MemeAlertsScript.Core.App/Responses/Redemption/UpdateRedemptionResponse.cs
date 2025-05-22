using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus;

namespace MemeAlertsScript.Core.App.Responses.Redemption
{
    public class UpdateRedemptionResponse
    {
        public required UpdateRedemptionStatusResponse StatusResponse { get; init; }
    }
}
