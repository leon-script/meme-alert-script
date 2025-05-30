using Microsoft.Extensions.Options;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class TwitchStateService(IOptions<TwitchOptions> options) : ITwitchStateService
    {
        public string GetOAuthState()
        {
            return Guid.NewGuid().ToString("N");
        }

        public Uri CreateOAuthUrl(string state)
        {
            return new Uri($"https://id.twitch.tv/oauth2/authorize" +
                           $"?client_id={options.Value.AppId}" +
                           $"&redirect_uri={options.Value.RedirectUri}" +
                           $"&response_type=code" +
                           $"&scope={string.Join("+", options.Value.Scopes)}" +
                           $"&state={state}");
        }

        public string GetRedirectUrl()
        {
            return options.Value.RedirectUri;
        }
    }
}
