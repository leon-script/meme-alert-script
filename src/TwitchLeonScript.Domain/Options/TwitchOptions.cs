namespace TwitchLeonScript.Domain.Options
{
    public sealed class TwitchOptions
    {
        public string AppId { get; set; } = string.Empty;
        public string AppSecret { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string[] Scopes { get; set; } = [];
    }
}
