namespace PdfGenerator.Shared.Binary;

/// <summary>
/// The content of the file.
/// </summary>
public interface IFileContent : IDisposable
{
    /// <summary>
    /// The name of the file.
    /// </summary>
    string Filename { get; }

    /// <summary>
    /// The media type of the file content.
    /// </summary>
    string MediaType { get; }

    /// <summary>
    /// The content of the file.
    /// </summary>
    Stream Content { get; }

    /// <summary>
    /// The length of the file.
    /// </summary>
    long? Length { get; }
}
