namespace PdfGenerator.Application.PdfConversions.Services;

/// <summary>
/// Provides an interface for generating PDF documents from HTML content.
/// </summary>
public interface IPdfGenerator
{
    /// <summary>
    /// Asynchronously generates a PDF document from the specified HTML content.
    /// </summary>
    /// <param name="html">The HTML content to convert into a PDF document.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, which, upon completion, returns
    /// a <see cref="Stream"/> containing the generated PDF document.</returns>
    Task<Stream> FromHtmlAsync(string html);
}