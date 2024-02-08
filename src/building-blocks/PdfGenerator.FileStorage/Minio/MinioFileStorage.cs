using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PdfGenerator.FileStorage.Abstractions;
using PdfGenerator.Shared;
using PdfGenerator.Shared.Binary;
using Polly;
using Polly.Retry;

namespace PdfGenerator.FileStorage.Minio;

/// <summary>
/// Implements an API facade for MinIO file storage.
/// </summary>
public class MinioFileStorage : IFileStorageService
{
    private readonly IMinioClient _client;
    private readonly MinioOptions _options;
    private readonly ILogger _logger;
    private readonly AsyncRetryPolicy _minioCallPolicy;

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioFileStorage"/> class.
    /// </summary>
    /// <param name="logger">Logger instance for logging service-related messages.</param>
    /// <param name="options">Options containing configuration settings.</param>
    public MinioFileStorage(
        ILogger<MinioFileStorage> logger,
        IOptions<MinioOptions> options)
    {
        _logger = logger;
        _options = options.Value;

        _client = new MinioClient()
            .WithEndpoint(_options.Address)
            .WithCredentials(_options.AccessKey, _options.SecretKey)
            .WithSSL(false)
            .Build();

        _minioCallPolicy = Policy
            .Handle<BucketNotFoundException>()
            .RetryAsync(1, onRetryAsync: (_, _) => CreateBucketAsync());
    }

    /// <inheritdoc />
    public async Task<Result<string>> UploadAsync(
        string key,
        IFileContent fileContent,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _minioCallPolicy.ExecuteAsync(async () =>
            {
                fileContent.Content.Position = 0;
                
                var args = new PutObjectArgs()
                    .WithBucket(_options.BucketName)
                    .WithObject(key)
                    .WithContentType(fileContent.MediaType)
                    .WithStreamData(fileContent.Content);

                if (fileContent.Length is not null)
                {
                    args.WithObjectSize(fileContent.Length.Value);
                }

                await _client.PutObjectAsync(args, cancellationToken);

                return Result<string>.NewSuccess(CreateFileFullPath(_options.BucketName!, key));
            });
        }
        catch (Exception ex)
        {
            return Result<string>.NewFailure(ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task<Result<string>> UploadAsync(
        IFileContent fileContent,
        CancellationToken cancellationToken = default)
    {
        var key = Guid.NewGuid().ToString();
        return await UploadAsync(key, fileContent, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Result<IFileContent>> DownloadAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var (bucketName, filePath) = GetKeyParts(key);

            var statObjArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(filePath);
            var objStat = await _client.StatObjectAsync(statObjArgs, cancellationToken);

            Stream? content = null;

            var getObjArgs = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(filePath)
                .WithCallbackStream(stream =>
                {
                    content = new MemoryStream();
                    stream.CopyTo(content);
                    content.Position = 0;
                });

            await _client.GetObjectAsync(getObjArgs, cancellationToken);

            return Result<IFileContent>.NewSuccess(new FileContent(
                objStat.ObjectName,
                objStat.ContentType,
                content!,
                objStat.Size));
        }
        catch (Exception ex)
        {
            return Result<IFileContent>.NewFailure(ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task<Result> DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _minioCallPolicy.ExecuteAsync(async () =>
            {
                var args = new RemoveObjectArgs()
                    .WithBucket(_options.BucketName)
                    .WithObject(key);

                await _client.RemoveObjectAsync(args, cancellationToken);
                return Result.Success;
            });
        }
        catch (Exception ex)
        {
            return Result.NewFailure(ex.Message);
        }
    }

    private static string CreateFileFullPath(string bucketName, string fileIdentifier) =>
        $"{bucketName}/{fileIdentifier}";

    private Task CreateBucketAsync(CancellationToken cancellationToken = default)
    {
        var mbArgs = new MakeBucketArgs()
            .WithBucket(_options.BucketName);

        _logger.LogInformation("Creating bucket with name {BucketEntryName}.", _options.BucketName);
        return _client.MakeBucketAsync(mbArgs, cancellationToken);
    }

    private (string BucketName, string FilePath) GetKeyParts(string fileKey)
    {
        var parts = fileKey.Split('/');

        if (parts.Length == 1)
        {
            return (_options.BucketName!, fileKey);
        }

        return (parts.First(), string.Join('/', parts.Skip(1)));
    }
}
