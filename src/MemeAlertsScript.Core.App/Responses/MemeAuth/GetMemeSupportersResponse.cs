using MemeAlertsScript.Core.Meme.Models;

namespace MemeAlertsScript.Core.App.Responses.MemeAuth
{
    public class GetMemeSupportersResponse
    {
        public required List<MemeSupporter> Supporters { get; init; }
    }
}
