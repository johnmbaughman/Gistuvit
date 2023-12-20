using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gistuvit.Microsoft.Extensions.Hosting;

public static class HostBuilderLoggingExtensions
{
    public static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging((context,logBuilder) =>
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(context.Configuration).CreateLogger();
            logBuilder.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(context.Configuration).CreateLogger(), dispose: true);
            logBuilder.Services.AddLogging();
        });

        return hostBuilder.ConfigureServices((builder, services) =>
        {
            services.AddSingleton(Log.Logger);
        });
    }
}