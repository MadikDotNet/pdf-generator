using Microsoft.AspNetCore.Http;

namespace PdfGenerator.Shared.Binary;

/// <inheritdoc />
public class FileContent : IFileContent
{
    /// <summary>
    /// Constructor with parameters to create a <see cref="FileContent"/> object.
    /// </summary>
    /// <param name="filename">The name of the file.</param>
    /// <param name="mediaType">The media type of the file content.</param>
    /// <param name="content">The content of the file.</param>
    /// <param name="length">The length of the file.</param>
    public FileContent(string filename, string mediaType, Stream content, long? length)
    {
        Filename = filename;
        MediaType = mediaType;
        Content = content;
        Length = length;
    }

    /// <inheritdoc />
    public string Filename { get; set; }

    /// <inheritdoc />
    public string MediaType { get; set; }

    /// <inheritdoc />
    public Stream Content { get; set; }

    /// <inheritdoc />
    public long? Length { get; set; }

    /// <summary>
    /// Creates an instance of <see cref="FileContent"/> from an <see cref="IFormFile"/> object.
    /// </summary>
    /// <param name="formFile">The <see cref="IFormFile"/> object representing the uploaded file received from a form.</param>
    /// <returns>An instance of <see cref="FileContent"/> containing the file's information and data.</returns>
    public static FileContent FromFormFile(IFormFile formFile)
    {
        return new FileContent(formFile.FileName, formFile.ContentType, formFile.OpenReadStream(), formFile.Length);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Content.Dispose();
    }
}
