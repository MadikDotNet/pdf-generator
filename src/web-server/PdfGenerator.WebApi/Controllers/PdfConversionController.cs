using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Application.PdfConversions.Services;
using PdfGenerator.Shared.Binary;

namespace PdfGenerator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfConversionController(IPdfConversionService pdfConversionService) : ControllerBase
{
    /// <summary>
    /// Retrieves all PDF conversion records.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An IActionResult containing all PDF conversion records.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await pdfConversionService.GetAllPdfConversionsAsync(cancellationToken);

        return Ok(result);
    }
    
    /// <summary>
    /// Queues a new PDF conversion task based on the provided HTML content.
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert to PDF, provided as a form file.</param>
    /// <returns>An IActionResult indicating the operation result.</returns>
    [HttpPost("queue-conversion")]
    public async Task<IActionResult> QueueConversionAsync(IFormFile htmlContent)
    {
        await pdfConversionService.QueuePdfConversionAsync(FileContent.FromFormFile(htmlContent));

        return Ok();
    }
}