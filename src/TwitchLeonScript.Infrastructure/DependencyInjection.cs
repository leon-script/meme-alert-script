using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Options;
using TwitchLeonScript.Infrastructure.Contexts;
using TwitchLeonScript.Infrastructure.Listeners;
using TwitchLeonScript.Infrastructure.Services;

namespace TwitchLeonScript.Infrastructure
{
    public sealed class DependencyInjection
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            // Configurations
            services.Configure<TwitchOptions>(configuration.GetSection("Twitch"));

            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Twitch services
            services.AddTransient<ITwitchAppTokenApiService, TwitchAppTokenApiService>();
            services.AddTransient<ITwitchBroadcasterApiService, TwitchBroadcasterApiService>();
            services.AddTransient<ITwitchOAuthTokenApiService, TwitchOAuthTokenApiService>();
            services.AddTransient<ITwitchRedemptionApiService, TwitchRedemptionApiService>();
            services.AddTransient<ITwitchRewardApiService, TwitchRewardApiService>();
            services.AddTransient<ITwitchStateService, TwitchStateService>();

            // Meme services
            services.AddTransient<IMemeBonusApiService, MemeBonusApiService>();
            services.AddTransient<IMemeBroadcasterApiService, MemeBroadcasterApiService>();
            services.AddTransient<IMemeStateService, MemeStateService>();
            services.AddTransient<IMemeSupporterApiService, MemeSupporterApiService>();

            // Listeners
            services.AddSingleton<ITwitchEventListener, TwitchEventListener>();

            // Contexts
            services.AddSingleton<ITwitchAuthContext, TwitchAuthContext>();
            services.AddSingleton<IMemeAuthContext, MemeAuthContext>();

            // Http clients
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
