using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text.Json;
using TwitchLeonScript.Core.App.Mappings;
using TwitchLeonScript.Core.Common.Options;
using TwitchLeonScript.Core.Meme.Services;
using TwitchLeonScript.Core.Twitch.Listeners;
using TwitchLeonScript.Core.Twitch.Services;

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
            services.AddTransient<MemeBroadcasterService>();
            services.AddTransient<MemeSupporterService>();
            services.AddTransient<MemeBonusService>();

            // Listeners
            services.AddSingleton<TwitchWebsocketListener>();

            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RewardStatusProfile>();
            });

            // HttpClients
            services.AddHttpClient("MemeAlerts", client =>
            {
                client.BaseAddress = new Uri("https://memealerts.com/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Serializers
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
