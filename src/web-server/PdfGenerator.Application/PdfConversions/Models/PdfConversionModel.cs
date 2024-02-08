using PdfGenerator.Domain.PdfConversions;

namespace PdfGenerator.Application.PdfConversions.Models;

/// <summary>
/// Represents a pdf conversion process model,
/// </summary>
public class PdfConversionModel
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
    /// Gets or sets time when the conversion was created.
    /// </summary>
    public required DateTime CreatedAt { get; set; }
}