using System.Text;
using Hangfire;
using Microsoft.Extensions.Logging;
using PdfGenerator.Application.PdfConversions.Models;
using PdfGenerator.Domain.PdfConversions;
using PdfGenerator.FileStorage.Abstractions;
using PdfGenerator.Shared;
using PdfGenerator.Shared.Binary;

namespace PdfGenerator.Application.PdfConversions.Services;

/// <inheritdoc/>
public class PdfConversionService(
    IFileStorageService fileStorageService,
    IPdfConversionRepository pdfConversionRepository,
    ILogger<PdfConversionService> logger,
    IPdfGenerator pdfGenerator) : IPdfConversionService
{
    public Task<List<PdfConversionModel>> GetAllPdfConversionsAsync(CancellationToken cancellationToken)
    {
        return pdfConversionRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Result> QueuePdfConversionAsync(IFileContent htmlFileContent) // we trust to client :)
    {
        var result = await fileStorageService.UploadAsync($"{Guid.NewGuid()}.html", htmlFileContent);

        if (!result.IsSuccess)
        {
            logger.LogError(
                "Error occured while uploading html file content, error message: {ErrorMessage}",
                result.ErrorMessage);

            return result;
        }

        var conversion = PdfConversion.New(result.Value!, htmlFileContent.Filename);
        await pdfConversionRepository.AddAsync(conversion);

        BackgroundJob.Enqueue(() => ConvertAsync(conversion.Id));
        
        logger.LogInformation("[{ConversionId}] Conversion successfully queued", conversion.Id);

        return Result.Success;
    }

    public async Task ConvertAsync(Guid conversionId)
    {
        var conversion = await pdfConversionRepository.GetById(conversionId, CancellationToken.None);

        if (conversion is null)
        {
            logger.LogError("[{ConversionId}] PdfConversion not found", conversionId);
            return;
        }

        var originFile = await DownloadOriginAsync(conversion);

        var pdfContent = await GeneratePdfFromHtmlAsync(originFile);

        var resultPath = await UploadConversionAsync(conversion, pdfContent);

        await pdfConversionRepository.SaveSuccessResultAsync(conversion.Id, resultPath);

        logger.LogInformation("[{ConversionId}] Successfully converted", conversionId);
    }

    private async Task<IFileContent> DownloadOriginAsync(PdfConversionModel pdfConversion)
    {
        var htmlContentResult = await fileStorageService.DownloadAsync(pdfConversion.OriginFilePath);

        if (!htmlContentResult.IsSuccess)
        {
            logger.LogError(
                "[{ConversionId}] Error while downloading origin file, error message: {ErrorMessage}",
                pdfConversion.Id,
                htmlContentResult.ErrorMessage);

            throw new ArgumentException(htmlContentResult.ErrorMessage);
        }

        return htmlContentResult.Value!;
    }

    private async Task<string> UploadConversionAsync(PdfConversionModel pdfConversion, Stream conversionContent)
    {
        pdfConversion.ResultPath = $"{Guid.NewGuid()}.pdf";
        var resultFileContent = new FileContent(
            pdfConversion.ResultPath,
            "application/pdf",
            conversionContent,
            conversionContent.Length);

        var uploadResult = await fileStorageService.UploadAsync(pdfConversion.ResultPath, resultFileContent);

        if (!uploadResult.IsSuccess)
        {
            logger.LogError("[{ConversionId}] Error while uploading converted file", pdfConversion.Id);

            throw new ArgumentException(uploadResult.ErrorMessage);
        }

        return uploadResult.Value!;
    }

    private async Task<Stream> GeneratePdfFromHtmlAsync(IFileContent htmlContent)
    {
        var memoryStream = new MemoryStream();
        await htmlContent.Content.CopyToAsync(memoryStream);

        return await pdfGenerator.FromHtmlAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
    }
}