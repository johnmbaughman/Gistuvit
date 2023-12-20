using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gistuvit.Microsoft.Extensions.Hosting;

public static class HostingBuilderConfigurationExtensions
{
    public static IHostBuilder ConfigureAppConfiguration<T>(this IHostBuilder hostBuilder) where T : class, new()
    {
        hostBuilder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddJsonFile("appsettings.json");
        });

        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddSingleton(provider => new SettingsManager<T>(context.Configuration.GetSection("Application"), provider.GetRequiredService<CipherService>()));
        } );
    }
}