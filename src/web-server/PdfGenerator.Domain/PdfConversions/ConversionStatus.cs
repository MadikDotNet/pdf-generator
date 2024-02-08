namespace PdfGenerator.Domain.PdfConversions;

/// <summary>
/// Specifies the status of a conversion process.
/// </summary>
public enum ConversionStatus
{
    /// <summary>
    /// Indicates that the conversion process is currently underway.
    /// </summary>
    InProcess = 1,

    /// <summary>
    /// Indicates that the conversion process has been completed successfully.
    /// </summary>
    Processed = 2,
}