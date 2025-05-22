using MediatR;
using MemeAlertsScript.Core.Twitch.Notifications;
using MemeAlertsScript.WinForms.Forms;
using MemeAlertsScript.WinForms.UI.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchLeonScript.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.WinForms.Tokens.Storages;

namespace MemeAlertsScript.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            var provider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            var mainForm = provider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            Core.App.Program.Register(services, configuration);

            // Configurations
            services.AddSingleton<ITwitchTokenStorage, TwitchTokenStorage>();
            services.AddSingleton<IMemeTokenStorage, MemeTokenStorage>();

            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                //cfg.AddProfile<RedemptionProfile>();
            });

            // Forms
            services.AddSingleton<MainForm>();
            services.AddTransient<TwitchAuthForm>();
            services.AddTransient<MemeAuthForm>();
            services.AddTransient<RewardCreateForm>();

            // Notification
            services.AddSingleton<INotificationHandler<TwitchRedemptionReceived>>(sp => sp.GetRequiredService<MainForm>());
        }
    }
}
