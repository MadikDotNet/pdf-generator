using Microsoft.AspNetCore.SignalR;

namespace PdfGenerator.Infrastructure.PdfConversions;

public class PdfConversionHub : Hub
{
    public const string ConversionsUpdatedMethodName = "ConversionsUpdated";
    public const string HubRoute = "pdf-conversion-hub";
}