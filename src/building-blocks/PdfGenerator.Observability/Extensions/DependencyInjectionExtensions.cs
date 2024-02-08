using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace PdfGenerator.Observability.Extensions;

/// <summary>
/// Extension methods for configuring Serilog as the logging provider.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Configures Serilog as the logging provider for the application.
    /// </summary>
    /// <param name="appBuilder">The <see cref="IHostApplicationBuilder"/> to configure.</param>
    /// <param name="loggerConfigurator">An optional action to configure the Serilog <see cref="LoggerConfiguration"/>.
    /// If not provided, the configuration will be read from the application's configuration settings.</param>
    public static void RegisterSerilogLogger(
        this IHostApplicationBuilder appBuilder,
        Action<LoggerConfiguration>? loggerConfigurator = null)
    {
        loggerConfigurator ??= config =>
        {
            config.ReadFrom.Configuration(appBuilder.Configuration);
        };

        var loggerConfiguration = new LoggerConfiguration();
        var bootstrapLoggerConfiguration = new LoggerConfiguration();

        loggerConfigurator(loggerConfiguration);
        loggerConfigurator(bootstrapLoggerConfiguration);

        appBuilder.Logging.ClearProviders();
        appBuilder.Logging.AddSerilog(loggerConfiguration.CreateLogger());

        Log.Logger = bootstrapLoggerConfiguration.CreateBootstrapLogger();
    }
}