namespace TwitchLeonScript.Core.Twitch.Helpers
{
    public class TwitchOAuthHelper
    {
        public static string CreateOAuthState()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static Uri CreateOAuthUrl(string clientId, string redirectUri, string[] scopes, string state)
        {
            return new Uri($"https://id.twitch.tv/oauth2/authorize" +
                           $"?client_id={clientId}" +
                           $"&redirect_uri={redirectUri}" +
                           $"&response_type=code" +
                           $"&scope={string.Join("+", scopes)}" +
                           $"&state={state}");
        }
    }
}
