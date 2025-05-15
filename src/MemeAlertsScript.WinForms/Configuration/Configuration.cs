using MemeAlertsScript.Core.Models;
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
    }
}
