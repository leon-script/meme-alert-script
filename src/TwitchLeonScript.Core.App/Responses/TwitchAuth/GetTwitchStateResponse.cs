namespace TwitchLeonScript.Core.App.Responses.TwitchAuth
{
    public class GetTwitchStateResponse
    {
        public required string State { get; set; }
        public required Uri Uri { get; set; }
    }
}
