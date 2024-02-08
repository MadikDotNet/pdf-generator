using PdfGenerator.Application.PdfConversions.Models;
using PdfGenerator.Shared;
using PdfGenerator.Shared.Binary;

namespace PdfGenerator.Application.PdfConversions.Services;

/// <summary>
/// Provides an interface for a service that handles PDF conversion operations.
/// </summary>
public interface IPdfConversionService
{
    /// <summary>
    /// Asynchronously retrieves all PDF conversion records.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="PdfConversionModel"/> objects.</returns>
    Task<List<PdfConversionModel>> GetAllPdfConversionsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Queues an asynchronous task to convert HTML content into a PDF document.
    /// </summary>
    /// <param name="htmlFileContent">The HTML file content to be converted into a PDF.</param>
    /// <returns>A task representing the asynchronous operation of queuing the PDF conversion.</returns>
    Task<Result> QueuePdfConversionAsync(IFileContent htmlFileContent);
}