namespace TwitchLeonScript.WinForms.Tokens.Models
{
    public sealed class MemeOAuthToken
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
