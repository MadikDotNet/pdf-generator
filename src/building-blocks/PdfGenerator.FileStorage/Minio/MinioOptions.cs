namespace PdfGenerator.FileStorage.Minio;

/// <summary>
/// Options for connecting to the MinIO file storage.
/// </summary>
public class MinioOptions
{
    public string? BucketName { get; set; }
    public string? Address { get; set; }
    public string? AccessKey { get; set; }
    public string? SecretKey { get; set; }
}
