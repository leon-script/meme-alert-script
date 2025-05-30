using Microsoft.Extensions.DependencyInjection;

namespace TwitchLeonScript.Application
{
    public static class DependencyInjection
    {
        public static void Register(IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        }
    }
}
