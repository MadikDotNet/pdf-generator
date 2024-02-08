namespace PdfGenerator.Shared.Exceptions;

/// <summary>
/// Represents errors that occur when a required configuration key is not defined in the application's configuration.
/// </summary>
public class RequiredConfigNotDefined(string configKey)
    : Exception($"Required config not defined, config key: {configKey}");