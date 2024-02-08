namespace PdfGenerator.Domain.PdfConversions;

/// <summary>
/// Represents a pdf conversion process, detailing the conversion's unique identifier,
/// status, original file path, result file path, and any error message.
/// </summary>
public class PdfConversion
{
    /// <summary>
    /// Gets or sets the unique identifier for the conversion.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the current status of the conversion.
    /// </summary>
    public ConversionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets origin file name.
    /// </summary>
    public required string OriginFileName { get; set; }

    /// <summary>
    /// Gets or sets the file path of the document being converted. This property is required.
    /// </summary>
    public required string OriginFilePath { get; set; }

    /// <summary>
    /// Gets or sets the file path for the result of the conversion. This property can be null if the conversion is not completed.
    /// </summary>
    public string? ResultPath { get; set; }

    /// <summary>
    /// Gets or sets the error message, if any, that occurred during the conversion.
    /// This property can be null if the conversion completed successfully or has not yet resulted in an error.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="PdfConversion"/> class with a unique identifier, sets the initial status to 'InProcess', and initializes the origin file path.
    /// </summary>
    /// <param name="filePath">The file path of the document to be converted.</param>
    /// <param name="fileName">The file name of the document to be converted.</param>
    /// <returns>A new <see cref="PdfConversion"/> object initialized with the provided file path and default values for other properties.</returns>
    public static PdfConversion New(string filePath, string fileName)
    {
        return new PdfConversion
        {
            Id = Guid.NewGuid(),
            OriginFilePath = filePath,
            OriginFileName = fileName,
            Status = ConversionStatus.InProcess
        };
    }
}
