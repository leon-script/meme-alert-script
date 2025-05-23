namespace TwitchLeonScript.Core.Common.Options
{
    public class TwitchOptions
    {
        public string AppId { get; set; } = string.Empty;
        public string AppSecret { get; set; } = string.Empty;
        public string BroadcasterName { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string[] Scopes { get; set; } = Array.Empty<string>();
    }
}
