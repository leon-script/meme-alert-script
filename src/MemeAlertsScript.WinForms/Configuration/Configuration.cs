using Microsoft.Extensions.Configuration;

namespace MemeAlertsScript.WinForms.Configs
{
    public static class Configuration
    {
        public static IConfigurationRoot Load()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Configuration");

            return new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();
        }

        public static TwitchSettings LoadTwitchSettings()
        {
            var config = Load();
            return config.GetSection("Twitch").Get<TwitchSettings>()!;
        }

        public class TwitchSettings
        {
            public string AppId { get; set; } = string.Empty;
            public string AppSecret { get; set; } = string.Empty;
            public string BroadcasterName { get; set; } = string.Empty;
            public string RedirectUri { get; set; } = string.Empty;
            public string[] Scopes { get; set; } = Array.Empty<string>();
        }
    }
}
