using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfGenerator.FileStorage.Abstractions;
using PdfGenerator.FileStorage.Minio;
using PdfGenerator.Shared.Exceptions;

namespace PdfGenerator.FileStorage.Extensions;

/// <summary>
/// Provides extension methods for IServiceCollection for registering services related to file storage.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers services for working with file storage.
    /// </summary>
    /// <param name="services">IServiceCollection to register services.</param>
    /// <param name="configuration">The application configuration.</param>
    public static void RegisterMinioFileStorage(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        const string configurationKey = "MinIO";
        var optionsSection = configuration.GetSection(configurationKey);

        var options = optionsSection.Get<MinioOptions>();

        _ = options?.BucketName ?? throw new RequiredConfigNotDefined($"{configurationKey}.BucketName");
        _ = options.Address ?? throw new RequiredConfigNotDefined($"{configurationKey}.Address");
        _ = options.AccessKey ?? throw new RequiredConfigNotDefined($"{configurationKey}.AccessKey");
        _ = options.SecretKey ?? throw new RequiredConfigNotDefined($"{configurationKey}.SecretKey");

        services.Configure<MinioOptions>(optionsSection);
        services.AddSingleton<IFileStorageService, MinioFileStorage>();
    }
}
