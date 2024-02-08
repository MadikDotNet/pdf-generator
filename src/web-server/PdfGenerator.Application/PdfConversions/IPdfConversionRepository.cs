using PdfGenerator.Application.PdfConversions.Models;
using PdfGenerator.Domain.PdfConversions;

namespace PdfGenerator.Application.PdfConversions;

/// <summary>
/// Defines the contract for repository operations on PdfConversion entities.
/// </summary>
public interface IPdfConversionRepository
{
    /// <summary>
    /// Asynchronously retrieves all PdfConversion entities from the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of PdfConversionModel objects.</returns>
    Task<List<PdfConversionModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Asynchronously retrieves a PdfConversion entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the PdfConversion entity to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the PdfConversionModel object if found; otherwise, null.</returns>
    Task<PdfConversionModel?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously adds a new PdfConversion entity to the repository.
    /// </summary>
    /// <param name="pdfConversion">The PdfConversion entity to be added to the repository.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(PdfConversion pdfConversion);

    /// <summary>
    /// Asynchronously updates the result path of an existing PdfConversion entity.
    /// </summary>
    /// <param name="conversionId">The unique identifier of the PdfConversion entity to update.</param>
    /// <param name="resultPath">The new result path to be set for the PdfConversion entity.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveSuccessResultAsync(Guid conversionId, string resultPath);
}
