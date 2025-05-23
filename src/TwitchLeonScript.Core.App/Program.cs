using TwitchLeonScript.Core.Common.Options;
using TwitchLeonScript.Core.Meme.Services;
using TwitchLeonScript.Core.Twitch.Listeners;
using TwitchLeonScript.Core.Twitch.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TwitchLeonScript.Core.App
{
    public static class Program
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            // Configurations
            services.Configure<TwitchOptions>(configuration.GetSection("Twitch"));

            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // Services
            services.AddTransient<TwitchAuthService>();
            services.AddTransient<TwitchRewardService>();
            services.AddTransient<TwitchRedemptionService>();
            services.AddTransient<MemeAuthService>();
            services.AddTransient<MemeSupporterService>();
            services.AddTransient<MemeBonusService>();

            // Listeners
            services.AddSingleton<TwitchWebsocketListener>();
        }
    }
}
