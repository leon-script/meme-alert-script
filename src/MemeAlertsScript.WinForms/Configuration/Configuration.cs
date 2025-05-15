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

        public static AppSettings LoadAppSettings()
        {
            var config = Load();
            return config.Get<AppSettings>()!;
        }

        public class AppSettings
        {
            public LoggingSettings Logging { get; set; } = new();
            public TwitchSettings Twitch { get; set; } = new();
        }

        public class LoggingSettings
        {
            public string LogLevel { get; set; } = "Information";
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
