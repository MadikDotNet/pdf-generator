using PdfGenerator.Shared;
using PdfGenerator.Shared.Binary;

namespace PdfGenerator.FileStorage.Abstractions;

/// <summary>
/// Defines the interface for a file storage service.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Asynchronously uploads a file to the storage.
    /// </summary>
    /// <param name="key">The key by which the file will be accessible in the storage.</param>
    /// <param name="fileContent">The content of the file to upload.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The result of the upload operation with the file key.</returns>
    Task<Result<string>> UploadAsync(string key, IFileContent fileContent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously uploads a file to the storage and returns the key by which the file is accessible.
    /// </summary>
    /// <param name="fileContent">The content of the file to upload.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The result of the upload operation with the file key.</returns>
    Task<Result<string>> UploadAsync(IFileContent fileContent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously downloads a file from the storage.
    /// </summary>
    /// <param name="key">The key of the file to download.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The result of the download operation with the file content.</returns>
    Task<Result<IFileContent>> DownloadAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a file from the storage.
    /// </summary>
    /// <param name="key">The key of the file to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The result of the delete operation.</returns>
    Task<Result> DeleteAsync(string key, CancellationToken cancellationToken = default);
}
