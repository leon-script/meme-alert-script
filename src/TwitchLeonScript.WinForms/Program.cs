using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchLeonScript.Domain.Events;
using TwitchLeonScript.Mapping.Profiles;
using TwitchLeonScript.Session.Storages;
using TwitchLeonScript.WinForms.Forms;

namespace TwitchLeonScript.WinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
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
            System.Windows.Forms.Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Modules
            Application.DependencyInjection.Register(services);
            Infrastructure.DependencyInjection.Register(services, configuration);

            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Application.DependencyInjection).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Infrastructure.DependencyInjection).Assembly);
            });

            // AutoMapper
            services.AddAutoMapper(cfg => cfg.AddProfile<RewardProfile>());

            // Session storages
            services.AddSingleton<TwitchSessionStorage>();
            services.AddSingleton<MemeSessionStorage>();

            // Forms
            services.AddSingleton<MainForm>();
            services.AddTransient<TwitchAuthForm>();
            services.AddTransient<MemeAuthForm>();
            services.AddTransient<RewardCreateForm>();
        }
    }
}
