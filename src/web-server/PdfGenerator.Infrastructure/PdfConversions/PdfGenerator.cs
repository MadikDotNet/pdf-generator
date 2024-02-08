using PdfGenerator.Application.PdfConversions.Services;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfGenerator.Infrastructure.PdfConversions;

/// <inheritdoc/>
public class PdfGenerator : IPdfGenerator
{
    /// <inheritdoc/>
    public async Task<Stream> FromHtmlAsync(string html)
    {
        await new BrowserFetcher().DownloadAsync();
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = ["--disable-gpu","--no-sandbox"]
        });
        var page = await browser.NewPageAsync();

        await page.SetContentAsync(html);

        var pdfOptions = new PdfOptions { Format = PaperFormat.A4 };

        return await page.PdfStreamAsync(pdfOptions);
    }
}