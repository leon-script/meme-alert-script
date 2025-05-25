using System.Net;

namespace TwitchLeonScript.Core.App.Commands.MemeBonus
{
    public sealed class SendMemeBonusResponse
    {
        public required bool IsSuccess { get; init; }
        public required HttpStatusCode StatusCode { get; init; }
    }
}
