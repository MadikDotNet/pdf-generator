using Microsoft.Extensions.DependencyInjection;
using PdfGenerator.Application.PdfConversions.Services;

namespace PdfGenerator.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to facilitate the registration of services.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers the PDF generator service within the service collection, allowing for dependency injection of the PDF generator functionality.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the PDF generator service will be added.</param>
    public static void RegisterPdfGenerator(this IServiceCollection services)
    {
        services.AddSingleton<IPdfGenerator, PdfConversions.PdfGenerator>();
    }

    /// <summary>
    /// Registers the PDF conversion service and its interface within the service collection, allowing for dependency injection of the PDF conversion functionality.
    /// This method adds the PDF conversion service as a scoped service, meaning a new instance will be created for each request scope.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the PDF conversion service and its interface will be added.</param>
    public static void RegisterConversionService(this IServiceCollection services)
    {
        services.AddScoped<PdfConversionService>();
        services.AddScoped<IPdfConversionService>(sp => sp.GetRequiredService<PdfConversionService>());
    }
}